using System;

namespace Hastnama.Ekipchi.Data.Group
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public Guid? OwnerId { get; set; }
        
        public bool IsPublic { get; set; }
        
    }
}