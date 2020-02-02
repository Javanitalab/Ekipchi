using System;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Event.Gallery
{
    public class EventGalleryDto
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public Guid EventId { get; set; }

        public Guid UserId { get; set; }
    }
}