namespace Hastnama.Ekipchi.Data.Role
{
    public class RolePermissionDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Parent { get; set; }
        public string Name { get; set; }
    }
}