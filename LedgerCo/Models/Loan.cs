using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.Models
{
    public class Loan
    {
        public Bank Bank { get; set; }
        public Borrower Borrower { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int LoanTenureInYear { get; set; }
        public decimal IntRate { get; set; }
        public List<Payment> Paymets { get; set; }

        public int LoanTenureInMonth
        {
            get
            {
                return LoanTenureInYear > 0 ? LoanTenureInYear * 12 : 0;
            }
        }

        public decimal InterestPayable
        {
            get
            {
                return PrincipalAmount * LoanTenureInYear * IntRate;
            }
        }

        public decimal LoanPayableAmount
        {
            get
            {
                return PrincipalAmount + InterestPayable;
            }
        }
    }
}
