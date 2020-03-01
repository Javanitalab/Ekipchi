using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hastnama.Ekipchi.Common.Enum;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class FinancialTransaction
    {
        public FinancialTransaction()
        {
            Payments = new List<Payment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TransactionId")]
        public Guid Id { get; set; }

        public bool BankTransaction { get; set; }

        public bool IsDeposit { get; set; }

        public double Amount { get; set; }

        public double BalanceBefore { get; set; }

        public double BalanceAfter { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public string Note { get; set; }

        public TransactionStatus TeTransactionStatus { get; set; }

        public TransactionType TransactionType { get; set; }
        public Guid? PayerId { get; set; }

        [ForeignKey(nameof(PayerId))]
        public virtual User Payer { get; set; }
        public Guid? ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public virtual User Receiver { get; set; }
        public Guid? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        public virtual List<Payment> Payments { get; set; }
    }
}