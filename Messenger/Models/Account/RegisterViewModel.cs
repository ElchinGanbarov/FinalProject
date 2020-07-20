using System.ComponentModel.DataAnnotations;


namespace Messenger.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Ad daxil edin..")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Soyad daxil edin..")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage ="Epoçt daxil edin..")]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Duzgun epoct daxil edin..")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Nömrəni daxil edin!")]
        [RegularExpression(@"((\+994[ /]*)?(\d[ /]*){8}\d)",
                           ErrorMessage = "Nömrəni düzgün daxil edin.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edin..")]
        [MaxLength(50, ErrorMessage = "Şifrə maximum 50 xarakter ola bilər")]
        [MinLength(6, ErrorMessage = "Şifrə minumum 6 xarakter ola bilər")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrə daxil edin..")]
        [MaxLength(50, ErrorMessage = "Şifrə maximum 50 xarakter ola bilər")]
        [MinLength(6, ErrorMessage = "Şifrə minumum 6 xarakter ola bilər")]
        [Compare("Password", ErrorMessage = "Şifrə və şifrə təsdiqi eyni olmalıdır")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        public bool IsEmailVerified { get; set; }

        [MaxLength(100)]
        public string EmailActivationCode { get; set; }
    }
}
