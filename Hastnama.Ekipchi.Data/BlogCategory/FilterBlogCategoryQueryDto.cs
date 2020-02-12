using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.BlogCategory
{
    public class FilterBlogCategoryQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}