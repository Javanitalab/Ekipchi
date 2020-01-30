using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Data.City
{
    public class CreateCityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public CountyDto County { get; set; }
        
    }
}