using System.Collections.Generic;
using Hastnama.Ekipchi.Data.Country;

namespace Hastnama.Ekipchi.Data.Province
{
    public class UpdateProvinceDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public IList<CountyDto> Counties { get; set; }

        
    }
}