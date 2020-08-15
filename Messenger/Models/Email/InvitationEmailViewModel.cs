using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Messenger.Models.Email
{
    public class InvitationEmailViewModel
    {
        //sender fullname
        public string SenderFullname { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please, enter valid email address")]
        public string ReceiverEmail { get; set; }
        
        [Required(ErrorMessage = "Email address is required")]
        [MinLength(10, ErrorMessage = "Text length must be min 10 character")]
        [MaxLength(200, ErrorMessage = "Text length must be max 200 character")]
        public string Text { get; set; }
    }
}
