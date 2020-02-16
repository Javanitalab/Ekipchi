using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class RolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PermissionId { get; set; }

        public int RoleId { get; set; }

        [ForeignKey(nameof(PermissionId))] public virtual Permission Permission { get; set; }

        [ForeignKey(nameof(RoleId))] public virtual Role Role { get; set; }
    }
}