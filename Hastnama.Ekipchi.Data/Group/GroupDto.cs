using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Group
{
    public class GroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerUsername { get; set; }
        public string OwnerAvatar { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public int Members { get; set; }

        public bool IsPublic { get; set; }

        public virtual List<UserDto> UsersInGroup { get; set; }

    }
}