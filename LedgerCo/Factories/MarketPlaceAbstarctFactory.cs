using LedgerCo.RequestProcessor;
using LedgerCo.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LedgerCo.Repository;
using LedgerCo.ResponseProcessor;
using LedgerCo.Models;

namespace LedgerCo.Factories
{
    public class MarketPlaceAbstarctFactory
    {
        public static string _dataSource = ConfigurationManager.AppSettings["DataSource"];
        public static string _outputType = ConfigurationManager.AppSettings["OutputType"];

        public static IRequest<Request> GetFactories(RequestType requestType)
        {
            DataProcessorFactory dataProcessorFactory = new DataProcessorFactory();
            ResponseProcessorFactory responseProcessorFactory = new ResponseProcessorFactory();

            IDataRepository dataRepository = dataProcessorFactory.GetDataProcessorInstance(Enum.Parse<DataSource>(_dataSource));
            IResponse response = responseProcessorFactory.GetResponseProcessorInstance(Enum.Parse<OutputType>(_outputType));

            RequestProcessorFactory requestProcessorFactory = new RequestProcessorFactory(dataRepository, response);
            var request = requestProcessorFactory.GetRequestProcessorInstance(requestType);

            return request;
        }
    }
}
