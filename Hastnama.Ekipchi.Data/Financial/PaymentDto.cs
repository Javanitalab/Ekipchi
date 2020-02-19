using System;

namespace Hastnama.Ekipchi.Data.Financial
{
    public class PaymentDto
    {
        public long Id { get; set; }
        
        public DateTime CreationDate { get; set; }

        public double Amount { get; set; }

        public string RequestId { get; set; }

        public int? VerificationStatus { get; set; }

        public string ReferenceId { get; set; }

        public int StatusId { get; set; }
    }
}