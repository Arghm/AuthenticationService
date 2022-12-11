using AuthenticationService.Contracts.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Models;
using AuthenticationService.Contracts.Authentication;

namespace AuthenticationService.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize(Policy = Politics.UserOperationsPolicy)]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserHandler _userHandler;
        public UserController(IUserHandler userHandler,
            ILogger<AuthController> logger = null)
        {
            _userHandler = userHandler ?? throw new ArgumentNullException(nameof(userHandler));
            _logger = logger ?? new NullLogger<AuthController>();
        }

        /// <summary>
        /// Create new user with Role-base authorization
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [Authorize(Policy = Politics.ReadWriteUsersClaims)]        
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            var userInfo = await _userHandler.CreateUser(user);
            return Ok(userInfo);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Policy = Politics.ReadWriteUsersClaims)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateModel user)
        {
            await _userHandler.UpdateUser(user);
            return NoContent();
        }

        [HttpGet]
        [Route("get-all")]
        [Authorize(Policy = Politics.ReadOnlyUsersInfo)]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userHandler.GetAllUsers();
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-id")]
        [Authorize(Policy = Politics.ReadOnlyUsersInfo)]
        public async Task<IActionResult> GetUserInfoById([FromQuery] string id)
        {
            var result = await _userHandler.GetUserInfoByUserId(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("get-by-name")]
        [Authorize(Policy = Politics.ReadOnlyUsersInfo)]
        public async Task<IActionResult> GetUserInfoByName([FromQuery] string userName)
        {
            var result = await _userHandler.GetUserInfoByUserName(userName);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
