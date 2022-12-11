using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationService.Contracts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AuthenticationService.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? new NullLogger<ErrorHandlerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                string result;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    UnauthorizedAccessException e => StatusCodes.Status401Unauthorized,
                    BadRequestException e => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                result = JsonSerializer.Serialize(response.StatusCode == StatusCodes.Status500InternalServerError 
                    ? new
                    {
                        error = "Internal server error, please try again later",
                        message = error?.Message
                    } 
                    : new
                    {
                        error = "Request error",
                        message = error?.Message
                    });

                _logger.LogError(result);

                await response.WriteAsync(result);
            }
        }
    }
}
