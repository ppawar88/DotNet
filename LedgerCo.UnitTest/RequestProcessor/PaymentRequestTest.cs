using LedgerCo.Models;
using LedgerCo.Repository;
using LedgerCo.RequestProcessor;
using LedgerCo.UnitTest.MockData;
using Moq;
using System;
using Xunit;

namespace LedgerCo.UnitTest.RequestProcessor
{
    public class PaymentRequestTest
    {
        private Mock<IDataRepository> _dataRepository;
        private MockedData _mockData;

        public PaymentRequestTest()
        {
            _dataRepository = new Mock<IDataRepository>();
            _mockData = new MockedData();
        }

        [Theory]
        [InlineData("PAYMENT IDIDI DALE")]
        [InlineData("PAYMENT IDIDI DALE AMT 3")]
        [InlineData("PAYMENT IDIDI DALE 500 EMI")]
        public void Process_Invalid_Request(string command)
        {
            var paymentRequest = new PaymentRequest(_dataRepository.Object);

            _dataRepository.Setup(d => d.ProcessPayment(_mockData.moqLoan));
            
            var ex = Assert.Throws<Exception>(() => paymentRequest.Process(command));
            Assert.Equal("Invalid Payment Command", ex.Message);
        }

        [Fact]
        public void Process_Valid_Request()
        {
            var paymentRequest = new PaymentRequest(_dataRepository.Object);

            string command = "PAYMENT IDIDI DALE 500 3";

            _dataRepository.Setup(d => d.ProcessPayment(It.IsAny<Loan>())).Returns(true);

            Assert.True(paymentRequest.Process(command));
        }
    }
}
