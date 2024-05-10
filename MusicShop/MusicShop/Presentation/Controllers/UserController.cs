using Microsoft.AspNetCore.Mvc;
using MusicShop.Infrastructure.Repository;
using MusicShop.Domain.Model.Core;
using MusicShop.Presentation.Common.DTOs.User;
using AutoMapper;
using MusicShop.Domain.Enums;
using MusicShop.Application.Services.ServiceHandler.User;
using Microsoft.AspNetCore.Authorization;
using MusicShop.Application.Common.Errors;


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
        
        public UserController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserServiceHandler userServiceHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userServiceHandler = userServiceHandler;
        }


        [AllowAnonymous]
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
            var allUsers = (await _unitOfWork.User.GetAllUsersIncludeRoleAsync()).ToList();
            var userResponse = _mapper.Map<IEnumerable<UserResponse>>(allUsers);
            return Ok(userResponse);
        }


        [Authorize(Policy = "Read")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {

            var user = await _unitOfWork.User.GetUserIncludeRoleAsync(id);

            if (user==null)
            {
                throw new UserNotFound();
            }

            var userResponse = _mapper.Map<UserEntity, UserResponse>(user);
            return Ok(userResponse);
        }


        [Authorize(Policy = "Create")]
        [Route(template: "CreateAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddAdminUser(UserRequest userRequest)
        {
            
            var roleIdentity=await _unitOfWork.Role.GetByIdAsync((int)Role.Admin);
            if (roleIdentity == null)
            {
                throw new InvalidRoleUser();
            }
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
            if (user == null)
            {
                throw new UserNotFound();
            }
            _unitOfWork.User.Remove(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
 

        [Authorize(Policy = "Update")]
        [HttpPut]
        [Route(template: "UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserRequestUpdate userDTO)
        {
            var user = await _unitOfWork.User.GetUserIncludeRoleAsync(userDTO.Id);
            if (user == null)
            {
                throw new UserNotFound();
            }

            var usersRoles = await _unitOfWork.Role.GetUserWithExistRole(userDTO.Role);
            if (usersRoles == null )
            {
                throw new InvalidRoleUser();
            }
            var roleIdentity = await _unitOfWork.Role.GetByIdAsync(usersRoles.Id);
            if (roleIdentity == null)
            {
                throw new InvalidRoleUser();
            }

            user.Roles = [roleIdentity];
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
