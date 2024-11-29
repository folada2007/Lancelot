using Lancelot.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Lancelot.Infrastructure.Extensions
{
    public static class IdentityExtension
    {
        public static RegistrationResult ExtensionsResult(this IdentityResult result) 
        {
            return new RegistrationResult
            {
                Success = result.Succeeded,
                Errors = result.Errors.ToDictionary(key => key.Code, val => val.Description)
            };
        }
    }
}
