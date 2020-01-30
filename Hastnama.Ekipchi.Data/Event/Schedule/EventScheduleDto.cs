using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.Data.Country;
using Hastnama.Ekipchi.Data.Region;

namespace Hastnama.Ekipchi.Data.City
{
    public class EventScheduleDto
    {
        public Guid Id { get; set; }
        
        public DateTime RegistrationDate { get; set; }

        public DateTime? EndRegistrationDate { get; set; }


        public DateTime EventDate { get; set; }

        public TimeSpan StartHour { get; set; }

        public TimeSpan EndHour { get; set; }

        public DateTime? RemoveEventInfoDate { get; set; }

    }
}