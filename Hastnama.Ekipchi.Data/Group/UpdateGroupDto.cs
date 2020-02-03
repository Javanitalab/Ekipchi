using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Category;
using Hastnama.Ekipchi.Data.Event;
using Hastnama.Ekipchi.Data.Host.AvailableDate;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Group
{
    public class UpdateGroupDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public UserDto User { get; set; }
        
        public bool IsDeleted { get; set; }

        public int Members { get; set; }

        public bool IsPublic { get; set; }
        public virtual List<UserDto> UsersInGroup { get; set; }

    }
}