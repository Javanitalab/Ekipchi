using System;

namespace Hastnama.Ekipchi.Data.Message
{
    public class FilterMessageQueryDto
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public Guid? SenderUserId { get; set; }

        public Guid ReceiverUserId { get; set; }
        
    }
}