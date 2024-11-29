using Lancelot.Application.DTOs.Results;
using Microsoft.AspNetCore.Identity;

namespace Lancelot.Server.Extensions
{
    public static class LoginExtensions
    {
        public static LoginResult ToLoginResult(this SignInResult login) 
        {
            return new LoginResult
            {
                Success = login.Succeeded,
                IsNotAllowed = login.IsNotAllowed,
            };
        }
    }
}
