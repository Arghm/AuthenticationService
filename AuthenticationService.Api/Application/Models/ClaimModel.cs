﻿namespace AuthenticationService.Api.Application.Models
{
    public class ClaimModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Issuer { get; set; }
    }
}
