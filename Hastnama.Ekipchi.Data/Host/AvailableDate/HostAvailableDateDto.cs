using System;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.Host.AvailableDate
{
    public class HostAvailableDateDto
    {
        public string FromHour { get; set; }

        public string ToHour { get; set; }

        public Days Days { get; set; }

        public DateTime DateTime { get; set; }
    }
}