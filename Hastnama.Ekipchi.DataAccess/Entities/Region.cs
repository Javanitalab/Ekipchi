using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("RegionId")]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? DistrictNumber { get; set; }

        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))] public virtual City City { get; set; }
    }
}