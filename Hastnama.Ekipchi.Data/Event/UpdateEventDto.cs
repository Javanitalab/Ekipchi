using Hastnama.Ekipchi.Common.Enum;
using System;

namespace Hastnama.Ekipchi.Data.Event
{
    public class UpdateEventDto
    {
        public Guid Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public Guid? HostId { get; set; }

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

        public double Income { get; set; }

        public int PinedTimes { get; set; }

        public int TotalAttendees { get; set; }
    }
}