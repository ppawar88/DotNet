using LedgerCo.Util;

namespace LedgerCo.Models
{
    public class Request
    {
        public RequestType RequestType { get; set; }

        public Loan Loan { get; set; }

        public Balance Balance { get; set; }

    }
}
