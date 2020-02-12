using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Host
{
    public class FilterHostQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}