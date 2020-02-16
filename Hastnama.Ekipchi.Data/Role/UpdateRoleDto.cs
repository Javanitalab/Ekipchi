using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hastnama.Ekipchi.Data.Role
{
    public class UpdateRoleDto
    {
        [Required] public string Name { get; set; }

        [Required] public List<int> PermissionId { get; set; }
    }
}