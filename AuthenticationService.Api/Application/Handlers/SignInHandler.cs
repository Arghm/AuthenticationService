using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using AuthenticationService.Api.Application.Models;
using AuthenticationService.Api.Application.Services.Jwt.Interfaces;
using AuthenticationService.Api.Application.Services.Password;
using AuthenticationService.Infrastructure.Entities;
using AuthenticationService.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationService.Api.Application.Handlers
{
    /// <inheritdoc/>
    public class SignInHandler : ISignInHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenRepository _accessTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger _logger;

        public SignInHandler(IUserRepository userRepository, 
            IAccessTokenRepository accessTokenRepository, 
            IJwtService jwtService, 
            IPasswordHasher passwordHasher, 
            ILogger<SignInHandler> logger = null)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _accessTokenRepository = accessTokenRepository ?? throw new ArgumentNullException(nameof(accessTokenRepository));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? new NullLogger<SignInHandler>();
        }

        /// <inheritdoc/>
        public async Task<TokenModel> GetToken(SignInEntity request)
        {
            ValidateSignInData(request);

            var user = await _userRepository.GetUserByUserName(request.UserName);

            if (user == null)
                throw new KeyNotFoundException("User Not found");

            if (user.IsDeleted || !user.IsActive)
                throw new UnauthorizedAccessException("User is blocked, contact admin");

            if (!_passwordHasher.IsValid(request.Password, user.Password))
                throw new UnauthorizedAccessException("Incorrect login or password");

            var claims = CollectUserClaims(user);

            var accessToken = _jwtService.GenerateToken(user.Id.ToString(), user.UserName, claims);

            var token = new TokenModel
            {
                AccessToken = accessToken.accessToken,
                Expiration = accessToken.expiration
            };
             
            var accessTokenEntity = new AccessTokenEntity
            {
                token = accessToken.accessToken,
                Expired = accessToken.expiration,
                //User = user,
                UserId = user.Id
            };

            try
            {
                await _accessTokenRepository.CreateAccessTokenAsync(accessTokenEntity);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Create access token error");
                throw;
            }
            return token;
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

        private void ValidateSignInData(SignInEntity signIn)
        {
            if (signIn == null)
                throw new ArgumentNullException(nameof(signIn));
            if (string.IsNullOrWhiteSpace(signIn.UserName))
                throw new ArgumentNullException(nameof(signIn.UserName));
            if (string.IsNullOrWhiteSpace(signIn.Password))
                throw new ArgumentNullException(nameof(signIn.Password));
        }
    }
}
