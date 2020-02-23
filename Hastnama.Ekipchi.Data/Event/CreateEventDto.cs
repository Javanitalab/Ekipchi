using Hastnama.Ekipchi.Common.Enum;
using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Event.Schedule;

namespace Hastnama.Ekipchi.Data.Event
{
    public class CreateEventDto
    {
        public int? CategoryId { get; set; }

        public string Name { get; set; }

        public Guid? HostId { get; set; }

        public string Description { get; set; }

        public EventType EventType { get; set; }

        public string Slug { get; set; }

        public EventAccessibility EventAccessibility { get; set; }

        public string Logo { get; set; }

        public string CoverPhoto { get; set; }

        public int MaximumAttendees { get; set; }

        public int MinimumAttendees { get; set; }

        public double Price { get; set; }

        public string TermsAndCondition { get; set; }

        public string Tags { get; set; }

        public List<string> EventGallery { get; set; }

        public EventScheduleDto EventSchedule { get; set; }
    }
}