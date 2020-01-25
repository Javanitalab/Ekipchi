using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.GuitarIranShop.DataAccess.Entities;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Message
    {
        public Message()
        {
            UserMessages = new List<UserMessage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MessageId")]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Message ReplayToMessage { get; set; }

        public virtual List<UserMessage> UserMessages { get; }
    }
}