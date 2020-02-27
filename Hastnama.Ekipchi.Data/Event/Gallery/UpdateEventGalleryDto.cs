using System;
using System.Text.Json.Serialization;

namespace Hastnama.Ekipchi.Data.Event.Gallery
{
    public class UpdateEventGalleryDto
    {
        
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Image { get; set; }

        public bool IsConfirmed { get; set; }
    }
}