using System;
using System.Text.Json.Serialization;

namespace Hastnama.Ekipchi.Data.Comment
{
    public class UpdateCommentDto
    {
        [JsonIgnore] public Guid? Id { get; set; }

        public string Content { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsDeleted { get; set; }
    }
}