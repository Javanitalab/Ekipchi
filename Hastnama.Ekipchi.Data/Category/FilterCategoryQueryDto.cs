using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Category
{
    public class FilterCategoryQueryDto : PagingOptions
    {
        public string Keyword { get; set; }
    }
}