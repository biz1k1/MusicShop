namespace MusicShop.Application.Services.Authentication
{
    public record AuthenticationResult
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
