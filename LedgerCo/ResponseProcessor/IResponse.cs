using LedgerCo.Models;

namespace LedgerCo.ResponseProcessor
{
    public interface IResponse
    {
        void DisplayBalance(Balance balance);
    }
}
