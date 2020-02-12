using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Event
{
    public class FilterEventQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}