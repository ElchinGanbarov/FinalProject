using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repository.Services
{
    class SearchModel
    {
    }

    public class SearchAccount
    {
        //public string Label { get; set; }
        public int Id { get; set; }
        //is friends
        public FriendshipStatus Friendship { get; set; }
        public string Img { get; set; }
        [MaxLength(101)]
        public string Label { get; set; } //fullname
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(100)]
        public string Website { get; set; }
        [MaxLength(50)]
        public string StatusText { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }

        public bool IsFriendRequestSender { get; set; }
    }

}
