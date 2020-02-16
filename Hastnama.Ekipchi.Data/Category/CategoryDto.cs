using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Host;

namespace Hastnama.Ekipchi.Data.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}