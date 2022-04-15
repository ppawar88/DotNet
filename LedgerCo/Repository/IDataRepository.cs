using LedgerCo.Models;

namespace LedgerCo.Repository
{
    public interface IDataRepository
    {
        bool ProcessLoan(Loan loan);
        bool ProcessPayment(Loan loan);
        Balance GetBalance(Balance balance);
    }
}
