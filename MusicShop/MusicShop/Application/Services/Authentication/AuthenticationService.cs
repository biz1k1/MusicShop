using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Errors;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Infrastructure.Data;
using MusicShop.Presentation.Common.DTOs.Authentication;
using FluentResults;
using Azure.Core;
using System.Diagnostics;
using AutoMapper;
using MusicShop.Application.Common.Models;
using MusicShop.Domain.Model.Core;
using MusicShop.Domain.Enums;
using MusicShop.Infrastructure.Repository;

namespace MusicShop.Application.Services.Authentication
{
    public class AuthenticationService : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            
        }
        public async Task<AuthenticationResult> Login(LoginRequest request)
        {
            var loginDTO = _mapper.Map<LoginRequest,LoginDTO>(request);

            //check
            var user = await _unitOfWork.User.GetUserByLoginAsync( loginDTO.Login);
            if (user == null || user.Login != loginDTO.Login|| user.Password!=loginDTO.Password )
            {
                throw new InvalidUserData();
            }
            //generate token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult
            {
                    Id=user.Id,
                    Login=request.Login,
                    Email=user.Email,
                    Token=token,
            };
        }

        public async Task<AuthenticationResult> Register(RegisterRequest request)
        {
            var registerDTO = _mapper.Map<RegisterRequest,RegisterDTO>(request);

            //check
            var userByEmail = await _unitOfWork.User.GetUserByLoginAsync(registerDTO.Login);
            if (userByEmail is not null)
            {
                throw new DuplicateEmailError();
            }
            //Role permissions
            var roleEntity = await _unitOfWork.Role.GetByIdAsync((int)Role.User);
            //create user
            var user = new UserEntity
            {
                Login=registerDTO.Login,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                Roles = [roleEntity]
            };
            _unitOfWork.User.Add(user);
            await _unitOfWork.SaveAsync();
            return new AuthenticationResult
            {

                Id = user.Id,
                Login=user.Login,
                Email = user.Email,
            };
        }
    }
}
