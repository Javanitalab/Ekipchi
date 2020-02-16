using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Faq
{
    public class FilterFaqQueryDto : PagingOptions
    {
        public string Question { get; set; }
    }
}