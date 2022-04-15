using LedgerCo.Models;
using LedgerCo.Repository;
using LedgerCo.RequestProcessor;
using LedgerCo.UnitTest.MockData;
using Moq;
using System;
using Xunit;

namespace LedgerCo.UnitTest.RequestProcessor
{
    public class LoanRequestTest
    {
        private Mock<IDataRepository> _dataRepository;
        private MockedData _mockData;

        public LoanRequestTest()
        {
            _dataRepository = new Mock<IDataRepository>();
            _mockData = new MockedData();
        }

        [Theory]
        [InlineData("LOAN IDIDI DALE")]
        [InlineData("LOAN IDIDI DALE AMT 5 4")]
        [InlineData("LOAN IDIDI DALE 5000 TERM 4")]
        [InlineData("LOAN IDIDI DALE 5000 5 RATE")]
        public void Process_Invalid_Request(string command)
        {
            var loanRequest = new LoanRequest(_dataRepository.Object);

            _dataRepository.Setup(d => d.ProcessLoan(_mockData.moqLoan));

            var ex = Assert.Throws<Exception>(() => loanRequest.Process(command));
            Assert.Equal("Invalid Loan Command", ex.Message);
        }

        [Fact]
        public void Process_Valid_Request()
        {
            var loanRequest = new LoanRequest(_dataRepository.Object);

            string command = "LOAN IDIDI DALE 5000 5 4";

            _dataRepository.Setup(d => d.ProcessLoan(It.IsAny<Loan>())).Returns(true);
            Assert.True(loanRequest.Process(command));
        }
    }
}
