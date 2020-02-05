using System;

namespace Hastnama.Ekipchi.Data.Comment
{
    public class FilterCommentQueryDto
    {
        
        public Guid? UserId { get; set; }

        public bool? IsConfirmed { get; set; }
    }
}