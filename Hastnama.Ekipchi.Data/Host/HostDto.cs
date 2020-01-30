using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class HostDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string ContactPerson { get; set; }

        public string CallNumber { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public PlaceType PlaceType { get; set; }

        public int Capacity { get; set; }

        public double PricePerHour { get; set; }

        public string Logo { get; set; }

        public string TermsAndCondition { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int EventCount { get; set; }

        public virtual List<string> Galleries { get; set; }

        public virtual List<CategoryDto> Categories { get; set; }

        public virtual List<EventDto> Events { get; set; }

        public virtual List<HostAvailableDateDto> HostAvailableDates { get; set; }

    }
}