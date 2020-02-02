using System;

namespace Hastnama.Ekipchi.Data.Message
{
    public class UpdateMessageDto
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Body { get; set; }
        
        public MessageDto Replay { get; set; }
        
        public DateTime? SeenDate { get; set; }

        public bool ReceiverHasDeleted { get; set; }

        public bool SenderHasDeleted { get; set; }
    }
}