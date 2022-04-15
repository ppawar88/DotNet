using LedgerCo.Models;
using System;

namespace LedgerCo.ResponseProcessor
{
    public class ConsoleResponse : IResponse
    {
        public void DisplayBalance(Balance balance)
        {
            //BANK_NAME BORROWER_NAME AMOUNT_PAID NO_OF_EMIS_LEFT
            string output = String.Format("{0} {1} {2} {3}",
                                                    balance.Bank.Name,
                                                    balance.Borrower.Name,
                                                    balance.RepaymentAmount,
                                                    balance.PendingEMICount);
            Console.WriteLine(output);
        }
    }
}
