using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.BlogCategory
{
    public class FilterBlogCategoryQueryDto : PagingOptions
    {
        public string Name { get; set; }

        public string Slug { get; set; }
    }
}