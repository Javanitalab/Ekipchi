using System;

namespace Hastnama.Ekipchi.Data.Message
{
    public class CreateMessageDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
        
        public Guid? SenderUserId { get; set; }

        public Guid ReceiverUserId { get; set; }
        
    }
}