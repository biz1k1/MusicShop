namespace MusicShop.Presentation.Common.DTOs.Authentication
{
    public record LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}