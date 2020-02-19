using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid TransactionId { get; set; }

        [ForeignKey(nameof(TransactionId))]
        public virtual FinancialTransaction FinancialTransaction { get; set; }


        public DateTime CreationDate { get; set; }

        public double Amount { get; set; }

        public string RequestId { get; set; }

        public int? VerificationStatus { get; set; }

        public string ReferenceId { get; set; }

        public int StatusId { get; set; }
    }
}