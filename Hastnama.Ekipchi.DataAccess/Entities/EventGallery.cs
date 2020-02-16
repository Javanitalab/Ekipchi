using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class EventGallery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EventId")]
        public Guid Id { get; set; }

        public string Image { get; set; }

        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))] public virtual Event Event { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))] public virtual User User { get; set; }

        public bool IsConfirmed { get; set; }
    }
}