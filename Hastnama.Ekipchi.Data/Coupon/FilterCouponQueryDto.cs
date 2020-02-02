using System;

namespace Hastnama.Ekipchi.Data.Coupon
{
    public class FilterCouponQueryDto
    {

        public string Code { get; set; }
        
        public long Amount { get; set; }

        public bool IsUnlimited { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

    }
}