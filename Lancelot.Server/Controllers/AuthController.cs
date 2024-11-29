using Lancelot.Application.Interfaces;
using Lancelot.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lancelot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        public AuthController(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] LoginUserDto login)
        {
            var result = await _authenticationServices.SignInAsync(login);

            if (result.Success)
            {
                return Ok(new { IsSuccess = true });
            }
            return BadRequest(new { IsSuccess = false, Message = "User not found. Please check your login or password and try again." });
        }

        [HttpGet("CheckAuth")]
        public IActionResult CheckAuth() 
        {
            if (User.Identity.IsAuthenticated) 
            {
                return Ok(new { IsSuccess = true, Message = $"{User.Identity.Name}" });
            }
            return BadRequest();
        }
    }
}
