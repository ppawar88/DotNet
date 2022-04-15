using LedgerCo.Models;
using LedgerCo.ResponseProcessor;
using System;
using System.IO;
using Xunit;

namespace LedgerCo.UnitTest.ResponseProcessor
{
    public class ConsoleResponseTest
    {
        [Fact]
        public void DisplayBalanceTest()
        {
            var consoleResponse = new ConsoleResponse();
            var sw = new StringWriter();

            //This will hold all Console.writeline output
            Console.SetOut(sw);

            var balance = new Balance()
            {
                Bank = new Bank() { Name = "IDIDI" },
                Borrower = new Borrower() { Name = "DALE" },
                RepaymentAmount = 10000,
                PendingEMICount = 5
            };

            consoleResponse.DisplayBalance(balance);

            //Display Balnce methos uses Console.WriteLine which internnaly add \r\n for new line 
            Assert.Equal("IDIDI DALE 10000 5\r\n", sw.ToString());
        }
    }
}
