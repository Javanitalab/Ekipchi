using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Group
    {
        public Group()
        {
            UserInGroups = new List<UserInGroup>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual User User { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDeleted { get; set; }

        public int Members { get; set; }

        public bool IsPublic { get; set; }

        public virtual List<UserInGroup> UserInGroups { get; }
    }
}