using System;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Event.Gallery
{
    public class CreateEventGalleryDto
    {
        public string Image { get; set; }
        
        public Guid UserId { get; set; }
    }
}