using LedgerCo.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LedgerCo.RequestProcessor
{
    public class FileParser
    {

        public List<string> GetInputCommands(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Input File not found");

            var commands = File.ReadAllLines(filePath).ToList();
            return commands;
        }

        public RequestType GetRequestType(string command)
        {
            if (command == null)
                throw new Exception("Invalid Command");

            var commandComponent = command.Split(" ");
            if (command.Length == 0 || commandComponent == null || commandComponent.Length == 0)
                throw new Exception("Input Command Missing");

            RequestType requestType;
            if (Enum.TryParse<RequestType>(commandComponent[0], true, out requestType))
            {
                return requestType;
            }
            else
                throw new Exception("Invalid Command");
        }
    }
}
