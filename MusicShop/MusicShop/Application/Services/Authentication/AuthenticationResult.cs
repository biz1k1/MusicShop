namespace MusicShop.Application.Services.Authentication
{
    public record AuthenticationResult
    {
        public string Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
