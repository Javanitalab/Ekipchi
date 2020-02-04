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

            UserInGroups = new List<UserInGroup>();

            Blogs = new List<Blog>();

            EventGalleries = new List<EventGallery>();

            Comments = new List<Comment>();

            ReceiverMessages = new List<UserMessage>();

            SenderMessage = new List<UserMessage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public Guid Id { get; set; }
        public string Mobile { get; set; }
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

        public bool Gender { get; set; }

        public virtual List<UserToken> UserTokens { get; }

        public virtual List<UserInGroup> UserInGroups { get; }

        public virtual List<Blog> Blogs { get; }

        public virtual List<EventGallery> EventGalleries { get; }

        public virtual List<Comment> Comments { get; }

        public virtual List<UserMessage> ReceiverMessages { get; }

        public virtual List<UserMessage> SenderMessage { get; }
        
        public virtual List<UserInRole> UserInRoles { get; set; }

    }
}