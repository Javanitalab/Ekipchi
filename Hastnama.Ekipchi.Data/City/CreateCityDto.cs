using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Data.City
{
    public class CreateCityDto
    {
        public string Name { get; set; }

        public int CountyId { get; set; }
    }
}