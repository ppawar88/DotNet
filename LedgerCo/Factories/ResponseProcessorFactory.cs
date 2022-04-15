using LedgerCo.ResponseProcessor;
using LedgerCo.Util;

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
