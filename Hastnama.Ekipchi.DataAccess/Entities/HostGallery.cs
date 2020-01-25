using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class HostGallery
    {

        public HostGallery()
        {
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Image { get; set; }

        public Guid HostId { get; set; }

        [ForeignKey(nameof(HostId))]
        public virtual Host Host { get; set; }
    }
}