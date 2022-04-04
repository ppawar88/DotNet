using LedgerCo.Factories;
using LedgerCo.Models;
using LedgerCo.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo.RequestProcessor
{
    public class FileParser
    {

        public List<string> GetInputCommands(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Input File not found.");

            var commands = File.ReadAllLines(filePath).ToList();
            return commands;
        }

        public void ProcessCommand(string command)
        {
            var commandComponent = command.Split(" ");
            if (commandComponent == null || commandComponent.Length == 0)
                throw new Exception("Input Command Missing");

            RequestType requestType;
            if (Enum.TryParse<RequestType>(commandComponent[0], true, out requestType))
            {
                IRequest<Request> request = MarketPlaceAbstarctFactory.GetFactories(requestType);
                var processCompleted = request.Process(command);
                if (!processCompleted)
                    throw new Exception("Command execution interrupted.");
            }
            else
                throw new Exception("Invalid Command");

        }
    }
}
