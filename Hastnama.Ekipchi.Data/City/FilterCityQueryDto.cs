using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.City
{
    public class FilterCityQueryDto : PagingOptions
    {
        public string CountyName { get; set; }

        public string Name { get; set; }
    }
}