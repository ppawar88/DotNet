using FluentAssertions;
using LedgerCo.Factories;
using LedgerCo.ResponseProcessor;
using Xunit;

namespace LedgerCo.UnitTest.FactoryTest
{
    public class ResponseProcessorFactoryTest
    {
        [Fact]
        public void GetResponseProcessorInstance_Console()
        {
            var responseProcessorFactory = new ResponseProcessorFactory();
            var objDataSource = responseProcessorFactory.GetResponseProcessorInstance(Util.OutputType.Console);
            objDataSource.Should().NotBeNull();
            objDataSource.Should().BeOfType<ConsoleResponse>();
        }

    }
}
