using Microsoft.AspNetCore.Mvc;
using MusicShop.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MusicShop.Domain.Model.Core;
using MusicShop.Presentation.Common.DTOs.User;
using AutoMapper;
using MusicShop.Domain.Enums;
using MusicShop.Domain.Model.Aunth;
using MusicShop.Application.Services.DbInitializer;
using MusicShop.Application.Services.ServiceHandler.User;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Application.Services.Authorization.PermissionService;


namespace MusicShop.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserServiceHandler _userServiceHandler;
        
        public UserController(IUnitOfWork unitOfWork,IMapper mapper,IUserServiceHandler userServiceHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userServiceHandler = userServiceHandler;
        }
        [Authorize(Policy = "Read")]
        [HttpPost]
        [Route(template: "SeedUser")]
        public async Task<IActionResult> TestControllerSeedUsers()
        {
            var result=await _userServiceHandler.DbInitializer();
            foreach(var item in result)
            {
                _unitOfWork.User.Add(item);
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [Authorize(Policy = "Read")]
        [HttpGet]
        [Route(template: "GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await _unitOfWork.User.GetAllUsersIncludeRoleAsync();
            var userResponse = _mapper.Map<List<UserEntity>, List<UserResponse>>((List<UserEntity>)allUsers);
            return Ok(userResponse);
        }
        [Authorize(Policy = "Read")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {

            var userById = await _unitOfWork.User.GetUserIncludeRoleAsync(id);
            if (userById.Email==null)
            {
                ModelState.AddModelError("return", "User not found");
                return ValidationProblem(ModelState);
            }
            var userResponse = _mapper.Map<UserEntity, UserResponse>(userById);
            userResponse.Role=userById.Roles.Select(x=>x.Name).FirstOrDefault();
            return Ok(userResponse);
        }

        [Authorize(Policy = "Create")]
        [Route(template: "CreateAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddAdminUser(UserRequest userRequest)
        {
            
            var roleIdentity=await _unitOfWork.Role.GetByIdAsync((int)Role.Admin);
            var user=_mapper.Map<UserRequest, UserEntity>(userRequest);
            user.Roles = [roleIdentity];
            _unitOfWork.User.Add(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [Authorize(Policy = "Delete")]
        [HttpDelete]
        [Route(template: "RemoveUser")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = await _unitOfWork.User.GetUserIncludeRoleAsync(id);
            _unitOfWork.User.Remove(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
        [Authorize(Policy = "Update")]
        [HttpPatch]
        [Route(template: "UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserEntity userDTO)
        {
            var user = _mapper.Map<UserEntity>(userDTO);
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
