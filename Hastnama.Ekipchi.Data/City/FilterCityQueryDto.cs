using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.City
{
    public class FilterCityQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}