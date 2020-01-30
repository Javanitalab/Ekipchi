using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.City
{
    public class EventGalleryDto
    {
        public Guid Id { get; set; }

        public string Image { get; set; }
        
        public Guid UserId { get; set; }

        public UserDto User { get; set; }

        public bool IsConfirmed { get; set; }
    }
}