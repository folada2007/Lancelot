using Lancelot.Application.DTOs;
using Lancelot.Shared.DTOs;

namespace Lancelot.Application.Interfaces
{
    public interface IUserServices
    {
        Task<RegistrationResult> AddUserAsync(UserRegistration userRegistration);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
