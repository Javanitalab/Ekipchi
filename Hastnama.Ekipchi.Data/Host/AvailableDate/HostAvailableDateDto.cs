using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class HostAvailableDateDto
    {
        public Guid Id { get; set; }

        public TimeSpan FromHour { get; set; }
        
        public TimeSpan ToHour { get; set; }

        public Days Days { get; set; }

        public DateTime DateTime { get; set; }

    }
}