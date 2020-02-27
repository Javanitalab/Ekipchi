using System;

namespace Hastnama.Ekipchi.Data.Event.Gallery
{
    public class EventGalleryDto
    {
        public Guid Id { get; set; }

        public string Image { get; set; }

        public bool IsConfirmed { get; set; }
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string UserAvatar { get; set; }
    }
}