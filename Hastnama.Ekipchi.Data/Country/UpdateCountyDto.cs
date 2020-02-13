using System.Text.Json.Serialization;

namespace Hastnama.Ekipchi.Data.Country
{
    public class UpdateCountyDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int ProvinceId { get; set; }
        
    }
}