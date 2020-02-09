using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Region
{
    public class FilterRegionQueryDto : PagingOptions
    {
        public string Name { get; set; }

        public int? DistrictNumber { get; set; }
        
        public string CityName { get; set; }
    }
}