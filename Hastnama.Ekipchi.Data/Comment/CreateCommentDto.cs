using System;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Comment
{
    public class CreateCommentDto
    {
        public Guid? ParentId { get; set; }

        public string Content { get; set; }

        public UserDto User { get; set; }
    }
}