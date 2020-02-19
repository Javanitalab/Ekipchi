using System;
using System.Collections.Generic;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Data.User;

namespace Hastnama.Ekipchi.Data.Financial
{
    public class FinancialTransactionDto
    {
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
        
        public UserDto Payer { get; set; }

        public UserDto Receiver { get; set; }

        public UserDto Author { get; set; }

        public List<PaymentDto> Payments { get; }

    }
}