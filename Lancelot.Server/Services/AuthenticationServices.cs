using Lancelot.Application.DTOs.Results;
using Lancelot.Application.Interfaces;
using Lancelot.Infrastructure.Identity.Entities;
using Lancelot.Server.Extensions;
using Lancelot.Shared.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Lancelot.Server.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly SignInManager<User> _signInManager;

        public AuthenticationServices(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<LoginResult> SignInAsync(LoginUserDto login)
        {
            var VerificationUser = await _signInManager.PasswordSignInAsync(
                   login.UserName,
                   login.Password,
                   login.RememberMe,
                   false);
            return VerificationUser.ToLoginResult();
        }
        
    }
}
