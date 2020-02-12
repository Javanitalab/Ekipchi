using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Country
{
    public class FilterCountyQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}