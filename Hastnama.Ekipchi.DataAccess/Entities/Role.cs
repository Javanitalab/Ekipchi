using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public sealed class Role
    {
        public Role()
        {
            RolePermissions = new List<RolePermission>();

            UserInRoles = new List<UserInRole>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsVital { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        public List<UserInRole> UserInRoles { get;set; }

        public static int Admin = 1;

        public static int User = 2;
    }
}