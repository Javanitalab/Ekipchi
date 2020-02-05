using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Hastnama.Ekipchi.Data.Host;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Event
{
    public class EventDto
    {
        public Guid Id { get; set; }

        public CategoryDto Category { get; set; }

        public string Name { get; set; }

        public HostDto Host { get; set; }

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

        public EventScheduleDto EventSchedule { get; set; }

        public EventGalleryDto EventGallery { get; set; }
        
        public IList<UserDto> Users { get; set; }
    }
}