using System;
using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Coupon
{
    public class FilterCouponQueryDto : PagingOptions
    {
        public string Code { get; set; }
        public long? Amount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsActive { get; set; }
    }
}