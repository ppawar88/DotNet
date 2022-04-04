using LedgerCo.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.Models
{
    public class Request
    {
        public RequestType RequestType { get; set; }

        public Loan Loan { get; set; }

        public Balance Balance { get; set; }

    }
}
