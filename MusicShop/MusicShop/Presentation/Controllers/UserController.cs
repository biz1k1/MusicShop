using Microsoft.AspNetCore.Mvc;
using MusicShop.Infrastructure.Repository;
using MusicShop.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Application.Services.Authentication.Identity;
using Microsoft.EntityFrameworkCore;
namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Authorize(Policy =IdentityData.Admin)]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route(template: "GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers =  _unitOfWork.User.GetAll();
            return Ok(allUsers);
        }
        [HttpGet]
        [Route(template: "GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userById = await _unitOfWork.User.FindByCondition(x=>x.Id==id).FirstOrDefaultAsync();
            return Ok(userById);
        }
        [Route(template: "AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            _unitOfWork.User.Add(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpDelete]
        [Route(template: "RemoveUser")]
        public async Task<IActionResult> RemoveUser(User user)
        {
            _unitOfWork.User.Remove(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [HttpPatch]
        [Route(template: "UpdateUser")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
