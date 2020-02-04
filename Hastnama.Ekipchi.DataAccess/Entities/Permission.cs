using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Permission
    {
        public Permission()
        {
            RolePermissions = new List<RolePermission>();

            Children = new List<Permission>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Permission Parent { get; set; }

        public virtual List<Permission> Children { get; }

        public virtual List<RolePermission> RolePermissions { get; }
    }
}