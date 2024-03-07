using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MusicShop.Presentation.Common.DTOs.Authentication;
using MusicShop.Application.Services.Authentication;

namespace MusicShop.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IMapper _mapper;
        public AuthenticationContoller(IAuthService authentication,IMapper mapper) {
            _auth = authentication;
            mapper = _mapper;
        }
        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _mapper.Map<AuthenticationResult,AuthenticationRequest > (request);
            return Ok(request);
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginRequest login)
        {
            return Ok(login);
        }
    }
}
