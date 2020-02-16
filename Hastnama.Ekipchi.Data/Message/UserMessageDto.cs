namespace Hastnama.Ekipchi.Data.Message
{
    public class UserMessageDto
    {
        public int Id { get; set; }

        public string SenderUserId { get; set; }

        public string SenderName { get; set; }

        public string ReceiverUserId { get; set; }

        public string ReceiverName { get; set; }

        public string SendDate { get; set; }

        public string SeenDate { get; set; }

        public bool ReceiverHasDeleted { get; set; }

        public bool SenderHasDeleted { get; set; }

        public int MessageId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int? ParentId { get; set; }
    }
}