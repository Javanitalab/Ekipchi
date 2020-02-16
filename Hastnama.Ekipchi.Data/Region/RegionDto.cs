namespace Hastnama.Ekipchi.Data.Region
{
    public class RegionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? DistrictNumber { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }
    }
}