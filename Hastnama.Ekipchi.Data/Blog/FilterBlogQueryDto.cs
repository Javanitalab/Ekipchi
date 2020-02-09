using System;
using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Blog
{
    public class FilterBlogQueryDto : PagingOptions
    {
        public string Title { get; set; }

        public Guid? UserId { get; set; }

        public string BlogCategoryName { get; set; }
    }
}