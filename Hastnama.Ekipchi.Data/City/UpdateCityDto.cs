using System.Collections.Generic;
using System.Text.Json.Serialization;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class UpdateCityDto
    {
        [JsonIgnore] public int Id { get; set; }

        public string Name { get; set; }

        public int CountyId { get; set; }

        public string CountyName { get; set; }

        public IList<RegionDto> Regions { get; set; }
    }
}