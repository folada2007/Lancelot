using Lancelot.Application.Interfaces;
using Lancelot.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lancelot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserRegistration user) 
        {
            if (!ModelState.IsValid) 
            {
                var ModelErrors = ModelState
                    .Where(c => c.Value.Errors.Count > 0)
                    .ToDictionary(key => key.Key, val => val.Value.Errors.Select(d => d.ErrorMessage).ToArray());

                return BadRequest(new { ModelErrors });
            }

            var result = await _userServices.AddUserAsync(user);

            if (result.Success) 
            {
                return Ok(new { IsSuccess = true });
            }
            return BadRequest(new { IdentityErrors = result.Errors.Select(v => v.Value)});

        }
    }
}
