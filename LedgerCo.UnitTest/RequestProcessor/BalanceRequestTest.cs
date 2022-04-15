using LedgerCo.Repository;
using LedgerCo.RequestProcessor;
using LedgerCo.ResponseProcessor;
using LedgerCo.UnitTest.MockData;
using Moq;
using System;
using Xunit;

namespace LedgerCo.UnitTest.RequestProcessor
{
    public class BalanceRequestTest
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<IResponse> _response;
        private MockedData _mockData;

        public BalanceRequestTest()
        {
            _dataRepository = new Mock<IDataRepository>();
            _response = new Mock<IResponse>();
            _mockData = new MockedData();
        }

        [Theory]
        [InlineData("BALANCE IDIDI DALE")]
        [InlineData("BALANCE IDIDI DALE EMINo")]
        public void Process_Invalid_Request(string command)
        {
            var balanceRequest = new BalanceRequest(_dataRepository.Object, _response.Object);

            _dataRepository.Setup(d => d.GetBalance(_mockData.moqBalance)).Returns(_mockData.moqBalance);
            _response.Setup(r => r.DisplayBalance(_mockData.moqBalance));

            var ex = Assert.Throws<Exception>(() => balanceRequest.Process(command));
            Assert.Equal("Invalid Balance Command", ex.Message);
        }

        [Fact]
        public void Process_Valid_Request()
        {
            var balanceRequest = new BalanceRequest(_dataRepository.Object, _response.Object);

            string command = "BALANCE IDIDI DALE 2";

            _dataRepository.Setup(d => d.GetBalance(_mockData.moqBalance)).Returns(_mockData.moqBalance);
            _response.Setup(r => r.DisplayBalance(_mockData.moqBalance));

            Assert.True(balanceRequest.Process(command));
        }
    }
}
