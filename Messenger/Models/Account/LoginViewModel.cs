using System.ComponentModel.DataAnnotations;


namespace Messenger.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-poçt vacibdir")]
        [MaxLength(50, ErrorMessage = "E-poçt maximum 50 xarakter ola bilər")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt daxil edin")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə vacibdir")]
        [MaxLength(50, ErrorMessage = "Şifrə maximum 50 xarakter ola bilər")]
        [MinLength(6, ErrorMessage = "Şifrə minumum 6 xarakter ola bilər")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
