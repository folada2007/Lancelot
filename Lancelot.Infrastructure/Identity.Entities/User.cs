using Microsoft.AspNetCore.Identity;

namespace Lancelot.Infrastructure.Identity.Entities
{
    public class User:IdentityUser
    {
        public string Gender { get; set; }
    }
}
