using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class UserInGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))] public virtual User User { get; set; }

        public Guid GroupId { get; set; }

        [ForeignKey(nameof(GroupId))] public virtual Group Groups { get; set; }

        public DateTime JoinGroupDate { get; set; }
    }
}