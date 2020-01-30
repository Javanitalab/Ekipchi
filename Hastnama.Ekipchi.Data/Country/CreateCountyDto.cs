namespace Hastnama.Ekipchi.Data.Country
{
    public class CreateCountyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; }
        
    }
}