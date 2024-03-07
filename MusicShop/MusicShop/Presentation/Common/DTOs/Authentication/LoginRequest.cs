namespace MusicShop.Presentation.Common.DTOs.Authentication
{
    public record LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}