using FluentAssertions;
using LedgerCo.Factories;
using LedgerCo.Repository;
using Xunit;

namespace LedgerCo.UnitTest.FactoryTest
{
    public class DataProcessorFactoryTest
    {
        [Fact]
        public void GetDataProcessorInstance_InMemory()
        {
            var dataProcessorFactory = new DataProcessorFactory();
            var objDataSource = dataProcessorFactory.GetDataProcessorInstance(Util.DataSource.InMemory);
            objDataSource.Should().NotBeNull();
            objDataSource.Should().BeOfType<InMemoryDataSource>();
        }

    }
}
