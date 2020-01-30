using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual List<CategoryDto> Categories { get; set; }

        public virtual List<HostDto> Hosts { get; set; }

    }
}