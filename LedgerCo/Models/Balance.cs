using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.Models
{
    public class Balance
    {
        public Balance()
        {
            Bank = new Bank();
            Borrower = new Borrower();
        }

        public Bank Bank { get; set; }
        public Borrower Borrower { get; set; }
        public int EMINumber { get; set; }
        public int PendingEMICount { get; set; }
        public decimal RepaymentAmount { get; set; }
    }
}
