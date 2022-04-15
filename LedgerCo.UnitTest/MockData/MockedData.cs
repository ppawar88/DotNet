using LedgerCo.Models;
using System.Collections.Generic;

namespace LedgerCo.UnitTest.MockData
{
    public class MockedData
    {
        public List<Loan> LoanLedgerDetail = new List<Loan>();

        public Loan moqLoan = new Loan
        {
            Bank = new Bank { Name = "IDIDI" },
            Borrower = new Borrower { Name = "Dale" },
            PrincipalAmount = 10000,
            LoanTenureInYear = 5,
            IntRate = 4
        };

        public Balance moqBalance = new Balance
        {
            Bank = new Bank { Name = "IDIDI" },
            Borrower = new Borrower { Name = "Dale" },
            EMINumber = 2
        };

        public Loan moqPayment = new Loan
        {
            Bank = new Bank { Name = "IDIDI" },
            Borrower = new Borrower { Name = "Dale2" },
            PrincipalAmount = 5000,
            LoanTenureInYear = 5,
            IntRate = 4,
            Payments = new List<Payment> { new Payment() { EMINumber = 2, RepaymentAmount = 500 } }
        };

        public List<Loan> GetDefaultLoanLedgerDetail()
        {
            LoanLedgerDetail.Add(moqLoan);
            LoanLedgerDetail.Add(moqPayment);

            return LoanLedgerDetail;
        }
    }
}
