using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Host
    {
        public Host()
        {
            HostGalleries = new List<HostGallery>();
            HostCategories = new List<HostCategory>();
            Events = new List<Event>();
            HostAvailableDates = new List<HostAvailableDate>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("HostId")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string ContactPerson { get; set; }

        public string CallNumber { get; set; }

        public string CoverPhoto { get; set; }

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

        public virtual List<HostGallery> HostGalleries { get; set; }

        public virtual List<HostCategory> HostCategories { get; set; }

        public virtual List<Event> Events { get; }

        public virtual List<HostAvailableDate> HostAvailableDates { get; set; }
    }
}