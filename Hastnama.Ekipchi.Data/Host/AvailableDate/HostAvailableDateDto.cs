using System;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.Host.AvailableDate
{
    public class HostAvailableDateDto
    {
        public DateTime? FromHour { get; set; }

        public DateTime? ToHour { get; set; }

        public Days Days { get; set; }

        public DateTime DateTime { get; set; }
    }
}