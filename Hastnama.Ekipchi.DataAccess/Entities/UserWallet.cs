using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hastnama.Ekipchi.DataAccess.Entities
{
    public class UserWallet
    {
        [Key, ForeignKey("User")]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public double TotalDeposit { get; set; }

        public double TotalSpend { get; set; }

        public double Balance { get; set; }

        public double Income { get; set; }

        public double Takeable { get; set; }

        public double TotalWithDraw { get; set; }
    }
}