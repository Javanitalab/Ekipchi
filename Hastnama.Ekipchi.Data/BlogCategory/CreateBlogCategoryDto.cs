namespace Hastnama.Ekipchi.Data.BlogCategory
{
    public class CreateBlogCategoryDto
    {
        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Cover{ get; set; }

        public string SlugPath { get; set; }

        public string LanguageCode { get; set; }

        public string Description { get; set; }

        public string LongDescription { get; set; }

        public string Logo  { get; set; }

        public int SortOrder { get; set; }
    }
}