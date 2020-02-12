using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Province
{
    public class FilterProvinceQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}