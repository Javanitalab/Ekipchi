using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Category
    {
        public Category()
        {
            Categories = new List<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CategoryId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Category> Categories { get; }
    }
}