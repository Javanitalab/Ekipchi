using System;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.Data.Host.AvailableDate
{
    public class HostAvailableDateDto
    {
        public TimeSpan FromHour { get; set; }

        public TimeSpan ToHour { get; set; }

        public Days Days { get; set; }

        public DateTime DateTime { get; set; }
    }
}