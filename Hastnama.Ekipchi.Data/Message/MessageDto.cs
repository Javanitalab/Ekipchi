using System;

namespace Hastnama.Ekipchi.Data.Message
{
    public class MessageDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        public MessageDto Replay { get; set; }

        public Guid? SenderUserId { get; set; }

        public Guid ReceiverUserId { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? SeenDate { get; set; }

        public bool ReceiverHasDeleted { get; set; }

        public bool SenderHasDeleted { get; set; }
    }
}