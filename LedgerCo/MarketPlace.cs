
using LedgerCo.RequestProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerCo
{
    public class MarketPlace
    {
        public void StartMarketPlace(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new Exception("Input File Missing");

            //Step1 : Read Input
            FileParser fileParser = new FileParser();
            var commands = fileParser.GetInputCommands(args[0]);

            //Step 2 : Process Input Commands 
            foreach (var command in commands)
                fileParser.ProcessCommand(command);

        }
    }
}
