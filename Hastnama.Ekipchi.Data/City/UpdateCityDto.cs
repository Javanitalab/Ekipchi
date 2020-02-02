using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class UpdateCityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int CountyId { get; set; }

        public string CountyName { get; set; }

        public IList<RegionDto> Regions { get; set; }

    }
}