using System;

namespace Hastnama.Ekipchi.Data.Coupon
{
    public class FilterCouponQueryDto
    {
        
        public long? Amount { get; set; }
        
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }

    }
}