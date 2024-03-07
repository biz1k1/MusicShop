﻿namespace MusicShop.Presentation.Common.DTOs.Authentication
{
    public record RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}