using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using Hastnama.Ekipchi.Common.Enum;
using ContentType = Hastnama.Ekipchi.Common.Enum.ContentType;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BlogId")]
        public int Id { get; set; }

        public Guid UseId { get; set; }

        [ForeignKey(nameof(UseId))]
        public virtual User User { get; set; }

        public int? BlogCategoryId { get; set; }

        [ForeignKey(nameof(BlogCategoryId))]
        public virtual BlogCategory BlogCategory { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string MetaKeyWord { get; set; }

        public string LanguageCode { get; set; }

        public string Slug { get; set; }

        public string ShortDescription { get; set; }

        public string Image { get; set; }

        public string Tag { get; set; }

        public string Link { get; set; }

        public bool IsPublish { get; set; }

        public ContentType ContentType { get; set; }

        public VideoType? VideoType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public int TotalUniqueView { get; set; }

        public int TotalView { get; set; }

        public bool IsSpecial { get; set; }
    }
}