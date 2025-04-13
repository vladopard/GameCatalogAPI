using AutoMapper;
using GameCatalogAPI.DTOS;
using GameCatalogAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO model)
        {
            var result = await _authenticationService.LoginAsync(model);
            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(new { Token = result.Token });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDTO model)
        {
            var result = await _authenticationService.RegisterAsync(model);
            if (!result.Success)
                return Conflict(result.Message);

            return Ok(new { Message = result.Message });
        }

    }
}
