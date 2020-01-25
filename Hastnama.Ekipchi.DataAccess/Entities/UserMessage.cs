using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.DataAccess.Entities;

namespace Hastnama.GuitarIranShop.DataAccess.Entities
{
    public class UserMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? SenderUserId { get; set; }

        public int ReceiverUserId { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? SeenDate { get; set; }

        public bool ReceiverHasDeleted { get; set; }

        public bool SenderHasDeleted { get; set; }

        public int MessageId { get; set; }

        [ForeignKey("SenderUserId"), Column(Order = 0)]
        public virtual User SenderUser { get; set; }

        [ForeignKey("ReceiverUserId"), Column(Order = 1)]
        public virtual User ReceiverUser { get; set; }

        [ForeignKey(nameof(MessageId))]
        public virtual Message Message { get; set; }
    }
}