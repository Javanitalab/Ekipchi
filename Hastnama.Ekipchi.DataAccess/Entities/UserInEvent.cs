using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class UserInEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))] public virtual User User { get; set; }

        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))] public virtual Event Event { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime JoinDate { get; set; }

        public bool IsExpired { get; set; }
    }
}