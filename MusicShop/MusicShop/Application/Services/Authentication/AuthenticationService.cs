using Microsoft.EntityFrameworkCore;
using MusicShop.Application.Common.Errors;
using MusicShop.Application.Common.Interfaces.Authentication;
using MusicShop.Domain.Model;
using MusicShop.Infrastructure.Data;
using MusicShop.Presentation.Common.DTOs.Authentication;
using FluentResults;
using Azure.Core;
using System.Diagnostics;
using AutoMapper;
using MusicShop.Application.Common.Models;

namespace MusicShop.Application.Services.Authentication
{
    public class AuthenticationService : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,DataContext context,IMapper mapper)
        {
            _db = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            
        }
        public AuthenticationResult Login(LoginRequest request)
        {
            var loginDTO = _mapper.Map<LoginRequest,LoginDTO>(request);

            //check
            var userByEmail = _db.Users.SingleOrDefault(x => x.Email == loginDTO.Email);
            if (userByEmail is not User user)
            {
                throw new InvalidEmail();
            }
            if (user.Password != user.Password)
            {
                throw new InvalidPassword();
            }
            //generate token
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
            return new AuthenticationResult
            {
                    Id=user.Id,
                    FirstName =user.FirstName,
                    LastName= user.LastName,
                    Email=" ",
                    Token=token
            };
        }

        public AuthenticationResult Register(RegisterRequest request)
        {
            var registerDTO = _mapper.Map<RegisterRequest,RegisterDTO>(request);

            //check
            var userByEmail = _db.Users.SingleOrDefault(x => x.Email == registerDTO.Email);
            if (userByEmail is not null)
            {
                throw new DuplicateEmailError();
            }

            //create user
            var user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Password = registerDTO.Password
            };
            _db.Users.AddAsync(user);
            _db.SaveChangesAsync();
            //generate token
            var token = _jwtTokenGenerator.GenerateToken(user.Id, registerDTO.FirstName, registerDTO.LastName);

            return new AuthenticationResult
            {
                Id = user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                Token=token
            };
        }
    }
}
