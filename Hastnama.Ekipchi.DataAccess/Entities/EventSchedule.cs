using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class EventSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EventScheduleId")]
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? EndRegistrationDate { get; set; }


        public DateTime EventDate { get; set; }

        public TimeSpan StartHour { get; set; }

        public TimeSpan EndHour { get; set; }

        public DateTime? RemoveEventInfoDate { get; set; }
    }
}