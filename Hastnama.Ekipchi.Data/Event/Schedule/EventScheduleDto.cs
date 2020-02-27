using System;

namespace Hastnama.Ekipchi.Data.Event.Schedule
{
    public class EventScheduleDto
    {
        public DateTime RegistrationDate { get; set; }

        public DateTime? EndRegistrationDate { get; set; }


        public DateTime EventDate { get; set; }

        public DateTime? StartHour { get; set; }

        public DateTime? EndHour { get; set; }

        public DateTime? RemoveEventInfoDate { get; set; }
    }
}