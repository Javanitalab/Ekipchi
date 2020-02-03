using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Comment
    {

        public Comment()
        {
            Children = new List<Comment>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CommentId")]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Comment ParentComment { get; set; }

        public string Content { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        
        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }


        public bool IsConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public virtual List<Comment> Children { get; }
    }
}