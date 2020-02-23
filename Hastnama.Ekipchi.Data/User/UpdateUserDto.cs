using System;
using System.Text.Json.Serialization;

namespace Hastnama.Ekipchi.Data.User
{
    public class UpdateUserDto
    {
        [JsonIgnore] public Guid Id { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
    }
}