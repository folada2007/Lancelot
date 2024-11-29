using System.ComponentModel.DataAnnotations;

namespace Lancelot.Shared.DTOs
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "You forgot to specify the name.")]
        [MinLength(2, ErrorMessage = "Min length 2 symbol")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You forgot to specify the Email.")]
        [EmailAddress(ErrorMessage = "Enter the correct email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You forgot to specify the Password.")]
        [MinLength(8, ErrorMessage = "Min length password 8 symbol")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You forgot to specify the Confirm Password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        public string Gender { get; set; } = "Male"; //aaaa потом пон да заменить надо ну заебал старина
    }
}
