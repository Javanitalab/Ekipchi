using System;
using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Blog
{
    public class FilterBlogQueryDto : PagingOptions
    {
        public string Keyword { get; set; }

        public Guid? UserId { get; set; }

    }
}