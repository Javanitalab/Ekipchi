using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CouponId")]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public int Count { get; set; }

        public int UsageCount { get; set; }

        public int TotalCount { get; set; }

        public long Amount { get; set; }

        public bool IsUnlimited { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}