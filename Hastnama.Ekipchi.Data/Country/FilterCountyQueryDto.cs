using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Country
{
    public class FilterCountyQueryDto : PagingOptions
    {
        public string ProvinceName { get; set; } 

        public string Name { get; set; }
    }
}