﻿namespace MusicShop.Presentation.Common.DTOs.Authentication
{
    public record RegisterRequest
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }

}