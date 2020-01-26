using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class BlogCategory
    {
        public BlogCategory()
        {
            Blogs = new List<Blog>();

            Children = new List<BlogCategory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BlogCategoryId")]
        public int Id { get; set; }

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

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual BlogCategory ParentCategory { get; set; }

        public virtual List<BlogCategory> Children { get; }

        public List<Blog> Blogs { get; }
    }
}