using LedgerCo.ResponseProcessor;
using LedgerCo.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.Factories
{
    public class ResponseProcessorFactory
    {
        public IResponse GetResponseProcessorInstance(OutputType outputType)
        {
            IResponse response = null;

            switch (outputType)
            {
                case OutputType.Console:
                    response = new ConsoleResponse();
                    break;
            }

            return response;
        }
    }
}
