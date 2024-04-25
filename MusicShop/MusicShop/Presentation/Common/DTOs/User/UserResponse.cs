using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Aunth;

namespace MusicShop.Presentation.Common.DTOs.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 
    }
}
