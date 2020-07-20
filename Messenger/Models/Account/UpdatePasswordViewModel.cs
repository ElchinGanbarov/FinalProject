using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.Account
{
    public class UpdatePasswordViewModel
    {
        [Required(ErrorMessage = "Current password is required")]
        [MaxLength(16, ErrorMessage = "Password length must be maximum 16 character")]
        [MinLength(6, ErrorMessage = "Password length must be minimum 6 character")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

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
