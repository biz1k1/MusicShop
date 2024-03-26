using Microsoft.AspNetCore.Mvc;

namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public async Task <IActionResult> GetAllUsers()
        {
            return Ok();
        }
        public async Task <IActionResult> GetUserById()
        {
            return Ok();
        }
        public async Task <IActionResult> AddUser()
        {
            return Ok();
        }
        public async Task <IActionResult> DeleteUser()
        {
            return Ok();
        }
        public async Task <IActionResult> UpdateUser()
        {
            return Ok();
        }
    }
}
