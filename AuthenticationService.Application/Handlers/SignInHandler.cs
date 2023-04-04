using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Handlers;
using AuthenticationService.Contracts.Services;
using AuthenticationService.Contracts.Repositories;
using AuthenticationService.Contracts.Repositories.Entities;
using AuthenticationService.Contracts.Models;

namespace AuthenticationService.Application.Handlers
{
    /// <inheritdoc/>
    public class LoginHandler : ILoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger _logger;

        public LoginHandler(IUserRepository userRepository, 
            IAccessTokenRepository accessTokenRepository, 
            IJwtService jwtService, 
            IPasswordHasher passwordHasher, 
            ILogger<LoginHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _accessTokenRepository = accessTokenRepository ?? throw new ArgumentNullException(nameof(accessTokenRepository));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? new NullLogger<LoginHandler>();
        }

        /// <inheritdoc/>
        public async Task<TokenModel> GetToken(LoginModel request)
        {
            ValidateLoginData(request);

            var user = await _userRepository.GetUserByUserName(request.UserName);

            if (user == null)
                throw new KeyNotFoundException("User Not found");

            if (user.IsDeleted || !user.IsActive)
                throw new UnauthorizedAccessException("User is blocked, contact admin");

            if (!_passwordHasher.IsValid(request.Password, user.Password))
                throw new UnauthorizedAccessException("Incorrect login or password");

            var claims = CollectUserClaims(user);

            try
            {
                TokenModel accessToken = _jwtService.GenerateToken(user.Id.ToString(), user.UserName, claims);
             
                var accessTokenEntity = new AccessTokenEntity
                {
                    token = accessToken.AccessToken,
                    Expired = accessToken.Expiration,
                    UserId = user.Id
                };

                await _accessTokenRepository.CreateAccessTokenAsync(accessTokenEntity);

                return accessToken;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Create access token error");
                throw;
            }
        }

        private IEnumerable<Claim> CollectUserClaims(UserEntity user)
        {
            List<Claim> claims = new List<Claim>();

            if (user.Roles != null)
                claims.AddRange(user.Roles.Select(userRoleEntity => new Claim(ClaimTypes.Role, userRoleEntity.Role.Role)));

            if (user.Claims != null)
                claims.AddRange(user.Claims.Select(userClaimEntity => new Claim(userClaimEntity.Claim.Type, userClaimEntity.Claim.Value)));

            return claims;
        }

        private void ValidateLoginData(LoginModel login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrWhiteSpace(login.UserName))
                throw new ArgumentNullException(nameof(login.UserName));
            if (string.IsNullOrWhiteSpace(login.Password))
                throw new ArgumentNullException(nameof(login.Password));
        }
    }
}
