﻿namespace NetWebApiTemplate.Api.Models
{
    public class LoginResponse
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
