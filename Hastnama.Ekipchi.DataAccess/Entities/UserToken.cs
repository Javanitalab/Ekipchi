using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class UserToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserTokenId")]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))] public virtual User User { get; set; }

        public string UserAgent { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Device { get; set; }

        public string Token { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsUsed { get; set; }
    }
}