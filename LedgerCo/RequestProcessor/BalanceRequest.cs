using LedgerCo.Models;
using LedgerCo.Repository;
using LedgerCo.ResponseProcessor;
using System;

namespace LedgerCo.RequestProcessor
{
    public class BalanceRequest : IRequest<Request>
    {
        private IDataRepository _dataRepository;
        public IResponse _response;

        public BalanceRequest(IDataRepository dataRepository, IResponse response)
        {
            this._dataRepository = dataRepository;
            this._response = response;
        }


        public bool Process(string command)
        {
            Request req = null;

            if (ValidRequest(command, out req))
            {
                var balance = this._dataRepository.GetBalance(req.Balance);
                this._response.DisplayBalance(balance);
            }
            else
                throw new Exception("Invalid Balance Command");

            return true;
        }

        public bool ValidRequest(string command, out Request req)
        {
            //  BALANCE BANK_NAME BORROWER_NAME EMI_NO

            req = new Request();

            var commandComp = command.Split(" ");
            int emiNumber;

            if (commandComp.Length < 4) //
                return false;

            if (!Int32.TryParse(commandComp[3], out emiNumber)) // EMI_NO
                return false;

            var balance = new Balance();
            balance.Bank.Name = commandComp[1]; // BANK_NAME
            balance.Borrower.Name = commandComp[2]; // BORROWER_NAME
            balance.EMINumber = emiNumber;

            req.Balance = balance;

            return true;
        }
    }
}
