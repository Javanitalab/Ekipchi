using System.Collections.Generic;

namespace Hastnama.Ekipchi.Data.Permission
{
    public class PermissionDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<PermissionDto> Children { get; set; }
    }
}