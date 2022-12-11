using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using AuthenticationService.Contracts.Handlers;
using AuthenticationService.Contracts.Models;

namespace AuthenticationService.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILoginHandler _loginHandler;
        public AuthController(ILoginHandler loginHandler,
            ILogger<AuthController> logger = null)
        {
            _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
            _logger = logger ?? new NullLogger<AuthController>();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                var result = await _loginHandler.GetToken(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                throw new Exception("Authentication error", e);
            }
        }
    }
}
