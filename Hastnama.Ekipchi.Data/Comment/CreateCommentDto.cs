using System;

namespace Hastnama.Ekipchi.Data.Comment
{
    public class CreateCommentDto
    {
        public Guid? ParentId { get; set; }
        public string Content { get; set; }
        public Guid EventId { get; set; }
    }
}