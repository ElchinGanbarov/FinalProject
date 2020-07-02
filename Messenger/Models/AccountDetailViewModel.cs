using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models
{
    public class AccountDetailViewModel
    {
        [Required(ErrorMessage = "Ad daxil edin..")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad daxil edin..")]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Epoçt daxil edin..")]
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Duzgun epoct daxil edin..")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Nömrəni daxil edin!")]
        [RegularExpression(@"((\+994[ /]*)?(\d[ /]*){8}\d)",
                           ErrorMessage = "Nömrəni düzgün daxil edin.")]
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Website { get; set; }
        public DateTime? LastLogin { get; set; }





    }
}
