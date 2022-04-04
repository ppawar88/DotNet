using LedgerCo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.Repository
{
    public interface IDataRepository
    {
        void ProcessLoan(Loan loan);
        void ProcessPayment(Loan loan);
        Balance GetBalance(Balance balance);
    }
}
