using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class HostAvailableDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public TimeSpan FromHour { get; set; }

        public Guid HostId { get; set; }

        [ForeignKey(nameof(HostId))] public virtual Host Host { get; set; }

        public TimeSpan ToHour { get; set; }

        public Days Days { get; set; }

        public DateTime DateTime { get; set; }
    }
}