using System;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Event.Gallery
{
    public class EventGalleryDto
    {
        public string Image { get; set; }


        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string UserAvatar { get; set; }
    }
}