using System;

namespace Hastnama.Ekipchi.Data.Blog
{
    public class FilterBlogQueryDto
    {
        public string Title { get; set; }

        public Guid? UserId { get; set; }

        public string BlogCategoryName { get; set; }
    }
}