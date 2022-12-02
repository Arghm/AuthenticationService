using System;

namespace AuthenticationService.Api.Application.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
