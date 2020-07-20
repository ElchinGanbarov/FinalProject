using System.ComponentModel.DataAnnotations;

namespace Messenger.Models.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Email length must be maximum 50 character")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class ResetPasswordConfirmViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(16, ErrorMessage = "Password length must be maximum 16 character")]
        [MinLength(6, ErrorMessage = "Password length must be minimum 6 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirm is required")]
        [MaxLength(16, ErrorMessage = "Password length must be maximum 16 character")]
        [MinLength(6, ErrorMessage = "Password length must be minimum 6 character")]
        [Compare("Password", ErrorMessage = "Password and password confirmation must be the same")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
