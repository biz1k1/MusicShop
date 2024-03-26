using Microsoft.AspNetCore.Mvc;
using MusicShop.Infrastructure.Repository;
using MusicShop.Domain.Model;
namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        UserController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task <IActionResult> GetAllUsers()
        {
            var allUsers = _unitOfWork.User.GetAll();
            return Ok(allUsers);
        }
        public async Task <IActionResult> GetUserById(int id)
        {
            var userById = _unitOfWork.User.GetById(id);
            return Ok(userById);
        }
        public async Task<IActionResult> AddUser(User user) {
            _unitOfWork.User.Add(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        public async Task <IActionResult> RemoveUser(User user)
        {
            _unitOfWork.User.Remove(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        public async Task <IActionResult> UpdateUser(User user)
        {
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
