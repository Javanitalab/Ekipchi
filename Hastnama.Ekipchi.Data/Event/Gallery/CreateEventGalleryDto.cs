using System;

namespace Hastnama.Ekipchi.Data.Event.Gallery
{
    public class CreateEventGalleryDto
    {
        public string Image { get; set; }

        public Guid EventId { get; set; }

        public Guid UserId { get; set; }
    }
}