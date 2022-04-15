using LedgerCo.Models;
using LedgerCo.Repository;
using System;

namespace LedgerCo.RequestProcessor
{
    public class PaymentRequest : IRequest<Request>
    {
        private IDataRepository _dataRepository;

        public PaymentRequest(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        public bool Process(string command)
        {
            Request req = null;

            if (ValidRequest(command, out req))
                return this._dataRepository.ProcessPayment(req.Loan);
            else
                throw new Exception("Invalid Payment Command");
        }

        public bool ValidRequest(string command, out Request req)
        {
            // PAYMENT BANK_NAME BORROWER_NAME LUMP_SUM_AMOUNT EMI_NO

            req = new Request();

            var commandComp = command.Split(" ");
            decimal repaymentAmount;
            int emiNumber;

            if (commandComp.Length < 5) //
                return false;

            if (!Decimal.TryParse(commandComp[3], out repaymentAmount)) //LUMP_SUM_AMOUNT
                return false;

            if (!Int32.TryParse(commandComp[4], out emiNumber)) //EMI_NO
                return false;

            var loan = new Loan();
            loan.Bank.Name = commandComp[1]; // BANK_NAME
            loan.Borrower.Name = commandComp[2]; // BORROWER_NAME

            loan.Payments.Add(new Payment
            {
                EMINumber = emiNumber,
                RepaymentAmount = repaymentAmount
            });
            req.Loan = loan;

            return true;
        }
    }
}
