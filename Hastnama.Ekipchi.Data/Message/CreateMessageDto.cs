namespace Hastnama.Ekipchi.Data.Message
{
    public class CreateMessageDto
    {
        public int? ParentId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Email { get; set; }
    }
}