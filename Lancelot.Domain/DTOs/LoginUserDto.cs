using System.ComponentModel.DataAnnotations;

namespace Lancelot.Shared.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "You forgot to specify the name.")]
        [MinLength(2, ErrorMessage = "Min length 2 symbol")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You forgot to specify the Password.")]
        [MinLength(8, ErrorMessage = "Min length password 8 symbol")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
