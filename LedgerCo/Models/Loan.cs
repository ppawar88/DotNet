using System;
using System.Collections.Generic;

namespace LedgerCo.Models
{
    public class Loan
    {
        public Loan()
        {
            Bank = new Bank();
            Borrower = new Borrower();
            Payments = new List<Payment>();
        }

        public Bank Bank { get; set; }
        public Borrower Borrower { get; set; }
        public decimal PrincipalAmount { get; set; }
        public int LoanTenureInYear { get; set; }
        public decimal IntRate { get; set; }
        public List<Payment> Payments { get; set; }

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
                return PrincipalAmount * LoanTenureInYear * IntRate / 100;
            }
        }

        public decimal LoanPayableAmount
        {
            get
            {
                return Math.Ceiling(PrincipalAmount + InterestPayable);
            }
        }

        public decimal EmiAmount
        {
            get
            {
                return Math.Ceiling(LoanPayableAmount / LoanTenureInMonth);
            }
        }

    }
}
