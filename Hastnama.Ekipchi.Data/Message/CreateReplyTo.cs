namespace Hastnama.Ekipchi.Data.Message
{
    public class CreateReplyTo
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int? ParentId { get; set; }
    }
}