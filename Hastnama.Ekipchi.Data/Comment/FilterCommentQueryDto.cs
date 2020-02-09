using System;
using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Comment
{
    public class FilterCommentQueryDto : PagingOptions
    {
        
        public Guid? UserId { get; set; }

        public bool? IsConfirmed { get; set; }
    }
}