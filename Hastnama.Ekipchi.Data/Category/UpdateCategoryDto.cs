using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}