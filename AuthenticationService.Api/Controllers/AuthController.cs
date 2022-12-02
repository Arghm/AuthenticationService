using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using AuthenticationService.Api.Application.Handlers;
using AuthenticationService.Infrastructure.Entities;

namespace AuthenticationService.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ISignInHandler _signInHandler;
        public AuthController(ISignInHandler signInHandler, 
            ILogger<AuthController> logger = null)
        {
            _signInHandler = signInHandler;
            _logger = logger ?? new NullLogger<AuthController>();
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInEntity request)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                request.IpAddress = ipAddress;
                var result = await _signInHandler.GetToken(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                throw new Exception("Ошибка авторизации", e);
            }
        }

        // Рефреш токена оказался оверхедным для внутренних систем. Может в будущем пригодится.

        //[HttpPost]
        //[Route("refresh")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        //{
        //    var result = await _mediator.Send(request);
        //    return Ok(result);
        //}
    }
}
