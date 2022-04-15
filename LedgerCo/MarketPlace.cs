
using LedgerCo.Factories;
using LedgerCo.Models;
using LedgerCo.Repository;
using LedgerCo.RequestProcessor;
using LedgerCo.ResponseProcessor;
using LedgerCo.Util;
using System;
using System.Configuration;

namespace LedgerCo
{
    public class MarketPlace
    {
        public static string _dataSource = ConfigurationManager.AppSettings["DataSource"];
        public static string _outputType = ConfigurationManager.AppSettings["OutputType"];

        public void StartMarketPlace(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new Exception("Input File Missing");

            //Step1 : Read Input
            FileParser fileParser = new FileParser();
            var commands = fileParser.GetInputCommands(args[0]);

            //Step 2 : Process Input Commands 
            foreach (var command in commands)
            {
                var requestType = fileParser.GetRequestType(command);
                IRequest<Request> request = this.GetFactory(requestType);
                request.Process(command);
            }

        }

        public IRequest<Request> GetFactory(RequestType requestType)
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
