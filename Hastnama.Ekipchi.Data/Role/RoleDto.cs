using System.Collections.Generic;

namespace Hastnama.Ekipchi.Data.Role
{
    public class RoleDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsVital { get; set; }

        public List<RolePermissionDto> RolePermissions { get; set; }
    }
}