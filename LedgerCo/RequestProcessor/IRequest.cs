namespace LedgerCo.RequestProcessor
{
    public interface IRequest<T>
    {
        bool Process(string command);

        bool ValidRequest(string command, out T data);
    }
}
