using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.City;
using Hastnama.Ekipchi.Data.Event.Gallery;
using Hastnama.Ekipchi.Data.Event.Schedule;
using Hastnama.Ekipchi.Data.Host;

namespace Hastnama.Ekipchi.Data.Event
{
    public class CreateEventDto
    {
        public virtual CategoryDto Category { get; set; }

        public string Name { get; set; }
        
        public virtual HostDto Host { get; set; }

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
        
        public virtual EventScheduleDto EventSchedule { get; set; }

        public virtual EventGalleryDto EventGallery { get; set; }
        
    }
}