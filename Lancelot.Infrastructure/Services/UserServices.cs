using Lancelot.Application.DTOs;
using Lancelot.Application.Interfaces;
using Lancelot.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Lancelot.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Lancelot.Shared.DTOs;

namespace Lancelot.Infrastructure.Services
{
    public class UserServices : IUserServices
    {
        public readonly UserManager<User> _userManager;

        public UserServices(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegistrationResult> AddUserAsync(UserRegistration userRegistration)
        {
            var user = new User
            {
                UserName = userRegistration.UserName,
                Email = userRegistration.Email,
                Gender = userRegistration.Gender
            };
            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            return result.ExtensionsResult();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users;
            return await users.Select(u => new UserDto
            {
                UserName = u.UserName,
                Id = u.Id,
                Gender = u.Gender,
                Email = u.Email
            }).ToListAsync();
        }
    }
}
