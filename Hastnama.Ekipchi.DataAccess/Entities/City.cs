using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class City
    {
        public City()
        {
            Regions = new List<Region>();
        }

        public int Id { get; set; }

        [Required] public string Name { get; set; }

        public int CountyId { get; set; }

        [ForeignKey(nameof(CountyId))] public virtual County County { get; set; }

        public virtual List<Region> Regions { get; set; }
    }
}