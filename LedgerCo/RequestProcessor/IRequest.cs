using LedgerCo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.RequestProcessor
{
    public interface IRequest<T>
    {
        bool Process(string command);

        bool ValidRequest(string command, out T data);
    }
}
