using System;
using System.Collections.Generic;

namespace Hastnama.Ekipchi.Data.Group
{
    public class UpdateGroupDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        
        public Guid OwnerId { get; set; }

        public bool IsDeleted { get; set; }
        
        public bool IsPublic { get; set; }
        public List<Guid> UsersInGroup { get; set; }
    }
}