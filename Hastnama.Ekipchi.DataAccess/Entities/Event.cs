using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Event
    {

        public Event()
        {
            EventSchedule = new EventSchedule();
            EventGallery = new EventGallery();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public string Name { get; set; }

        public Guid? HostId { get; set; }

        [ForeignKey(nameof(HostId))]
        public virtual Host Host { get; set; }

        public string Description { get; set; }

        public EventType EventType { get; set; }

        public string Slug { get; set; }


        public EventAccessibility EventAccessibility { get; set; }

        public string Image { get; set; }

        public string CoverPhoto { get; set; }

        public int MaximumAttendees { get; set; }

        public int MinimumAttendees { get; set; }

        public double Price { get; set; }

        public string TermsAndCondition { get; set; }

        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public int TotalView { get; set; }

        public int UniqueView { get; set; }

        public double Income { get; set; }

        public int PinedTimes { get; set; }

        public int TotalAttendees { get; set; }

        public virtual EventSchedule EventSchedule { get; }

        public virtual EventGallery EventGallery { get; }
    }
}