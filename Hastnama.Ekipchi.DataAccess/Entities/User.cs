using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class User
    {

        public User()
        {
            UserTokens = new List<UserToken>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Mobile { get; set; }

        public Role Role { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        [NotMapped] public string FullName => $"{Name} {Family}";

        public string Username { get; set; }

        public string ConfirmCode { get; set; }

        public string Avatar { get; set; }

        public UserStatus Status { get; set; }

        public bool IsPhoneConfirmed { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ExpiredVerificationCode { get; set; }

        public virtual List<UserToken> UserTokens { get; }
    }
}