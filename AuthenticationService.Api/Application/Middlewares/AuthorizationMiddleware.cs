using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AuthenticationService.Api.Application.Models;

namespace AuthenticationService.Api.Application.Middlewares
{
    // На будущее
    public class AuthorizationMiddleware
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next, IHttpClientFactory httpClient)
        {
            _next = next;
            _httpClient = httpClient;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader))
            {
                var tok = authHeader.Replace("Bearer ", "");
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
                var client = _httpClient.CreateClient("client");

                var response = await client.GetStringAsync(new Uri("https://localhost:5001/api/claims/get-user-claims/" + jwtToken.Subject));
                var body = JsonConvert.DeserializeObject<List<ClaimModel>>(response);
                var claims = body.Select(c => new Claim(c.Type, c.Value));
                context.User.AddIdentity(new ClaimsIdentity(claims));
            }

            await _next(context);
        }
    }
}
