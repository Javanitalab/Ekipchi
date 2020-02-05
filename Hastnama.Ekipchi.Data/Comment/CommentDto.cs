using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Comment
{
    public class CommentDto
    {
        public Guid? Id { get; set; }

        public Guid? ParentId { get; set; }

        public CommentDto ParentComment { get; set; }

        public string Content { get; set; }


        public UserDto User { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public List<CommentDto> Children { get; }
    }
}