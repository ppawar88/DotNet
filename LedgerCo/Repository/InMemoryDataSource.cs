using LedgerCo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedgerCo.Repository
{
    public sealed class InMemoryDataSource : IDataRepository
    {
        //InMemory Data
        private readonly List<Loan> _loanLedgerDetail = new List<Loan>();

        private static InMemoryDataSource _objInMemoryDataSource;

        static object _lock = new object();
        private InMemoryDataSource() { }

        public static InMemoryDataSource Instance
        {
            get
            {
                if (_objInMemoryDataSource != null)
                    return _objInMemoryDataSource;

                lock (_lock)
                {
                    if (_objInMemoryDataSource == null)
                        _objInMemoryDataSource = new InMemoryDataSource();

                    return _objInMemoryDataSource;
                }
            }
        }
        public IEnumerable<Loan> CheckForValidLoanAccount(string bankName, string borrowerName)
        {
            var loanDetails = _loanLedgerDetail.Where(l => l.Bank.Name == bankName
                                            && l.Borrower.Name == borrowerName);
            return loanDetails;
        }

        public Balance GetBalance(Balance balance)
        {
            var loanDetail = CheckForValidLoanAccount(balance.Bank.Name, balance.Borrower.Name).FirstOrDefault();

            if (loanDetail == null)
                throw new Exception("Loan detail not found for balance check");

            var lumsumPayments = loanDetail.Payments.Where(p => p.EMINumber <= balance.EMINumber);

            decimal totalLumsumPayment = 0;

            if (lumsumPayments.Any())
                totalLumsumPayment = lumsumPayments.Sum(p => p.RepaymentAmount);

            decimal emiPayment = Math.Ceiling(loanDetail.EmiAmount * balance.EMINumber);

            decimal balanceAmout = loanDetail.LoanPayableAmount - totalLumsumPayment - emiPayment;

            balance.RepaymentAmount = Math.Max(Math.Min(totalLumsumPayment + emiPayment, loanDetail.LoanPayableAmount), 0);
            balance.PendingEMICount = Convert.ToInt32(Math.Ceiling(balanceAmout > 0 ? balanceAmout / loanDetail.EmiAmount : 0));

            return balance;
        }

        public bool ProcessLoan(Loan loan)
        {
            if (CheckForValidLoanAccount(loan.Bank.Name, loan.Borrower.Name).Any())
                throw new Exception("Duplicate loan entry");

            _loanLedgerDetail.Add(loan);
            return true;
        }

        public bool ProcessPayment(Loan loan)
        {
            var loanDetail = CheckForValidLoanAccount(loan.Bank.Name, loan.Borrower.Name).FirstOrDefault();

            if (loanDetail == null)
                throw new Exception("Loan detail not found for payment");

            loanDetail.Payments.Add(loan.Payments.FirstOrDefault());
            return true;
        }
    }
}
