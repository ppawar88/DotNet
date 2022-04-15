using LedgerCo.Models;
using LedgerCo.Repository;
using System;

namespace LedgerCo.RequestProcessor
{
    public class LoanRequest : IRequest<Request>
    {
        private IDataRepository _dataRepository;

        public LoanRequest(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        public bool Process(string command)
        {
            Request req = null;

            if (ValidRequest(command, out req))
                return this._dataRepository.ProcessLoan(req.Loan);
            else
                throw new Exception("Invalid Loan Command");
        }

        public bool ValidRequest(string command, out Request req)
        {
            // LOAN BANK_NAME BORROWER_NAME PRINCIPAL NO_OF_YEARS RATE_OF_INTEREST

            req = new Request();

            var commandComp = command.Split(" ");
            decimal principalAmount, intRate;
            int loanTenure;

            if (commandComp.Length < 6)
                return false;

            if (!Decimal.TryParse(commandComp[3], out principalAmount)) //PRINCIPAL
                return false;

            if (!Int32.TryParse(commandComp[4], out loanTenure)) //NO_OF_YEARS
                return false;

            if (!Decimal.TryParse(commandComp[5], out intRate)) // RATE_OF_INTEREST
                return false;

            var loan = new Loan();

            loan.Bank.Name = commandComp[1]; // BANK_NAME
            loan.Borrower.Name = commandComp[2]; // BORROWER_NAME
            loan.PrincipalAmount = principalAmount;
            loan.LoanTenureInYear = loanTenure;
            loan.IntRate = intRate;

            req.Loan = loan;
            return true;
        }

    }
}