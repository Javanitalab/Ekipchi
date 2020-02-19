using System;
using Hastnama.Ekipchi.Common.General;

namespace Hastnama.Ekipchi.Data.Financial
{
    public class FilterFinancialTransactionQueryDto : PagingOptions
    {

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string PayerUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public string AuthorUsername { get; set; }
    }
}