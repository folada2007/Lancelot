using Lancelot.Application.DTOs.Results;
using Lancelot.Shared.DTOs;

namespace Lancelot.Application.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<LoginResult> SignInAsync(LoginUserDto login);
        Task LogOutAsync();
    }
}
