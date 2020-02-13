using System.Collections.Generic;
using System.Text.Json.Serialization;
using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Data.Province
{
    public class UpdateProvinceDto
    {
        
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public IList<CountyDto> Counties { get; set; }

        
    }
}