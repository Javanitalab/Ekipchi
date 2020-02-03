using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.Data.Host.AvailableDate;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Group
{
    public class CreateGroupDto
    {

        public string Name { get; set; }

        public Guid? OwnerId { get; set; }
        
        public bool IsPublic { get; set; }
        
    }
}