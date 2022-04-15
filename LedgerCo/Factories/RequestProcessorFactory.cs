using LedgerCo.Models;
using LedgerCo.Repository;
using LedgerCo.RequestProcessor;
using LedgerCo.ResponseProcessor;
using LedgerCo.Util;

namespace LedgerCo.Factories
{
    public class RequestProcessorFactory
    {
        private IDataRepository _dataRepository;
        public IResponse _response;

        public RequestProcessorFactory(IDataRepository dataRepository, IResponse response)
        {
            this._dataRepository = dataRepository;
            this._response = response;
        }

        public IRequest<Request> GetRequestProcessorInstance(RequestType requestType)
        {
            IRequest<Request> request = null;
            switch (requestType)
            {
                case RequestType.Loan:
                    request = new LoanRequest(this._dataRepository);
                    break;
                case RequestType.Payment:
                    request = new PaymentRequest(this._dataRepository);
                    break;
                case RequestType.Balance:
                    request = new BalanceRequest(this._dataRepository, this._response);
                    break;
            }

            return request;
        }
    }
}
