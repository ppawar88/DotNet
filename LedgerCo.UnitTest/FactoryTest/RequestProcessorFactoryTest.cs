using FluentAssertions;
using LedgerCo.Factories;
using LedgerCo.Repository;
using LedgerCo.RequestProcessor;
using LedgerCo.ResponseProcessor;
using Moq;
using Xunit;

namespace LedgerCo.UnitTest.FactoryTest
{
    public class RequestProcessorFactoryTest
    {
        private Mock<IDataRepository> _dataRepository;
        private Mock<IResponse> _response;

        public RequestProcessorFactoryTest()
        {
            _dataRepository = new Mock<IDataRepository>();
            _response = new Mock<IResponse>();
        }

        [Fact]
        public void GetRequestProcessorInstance_LoanType()
        {
            var requestProcessorFactory = new RequestProcessorFactory(_dataRepository.Object, _response.Object);

            var objRequestProcessor = requestProcessorFactory.GetRequestProcessorInstance(Util.RequestType.Loan);
            objRequestProcessor.Should().NotBeNull();
            objRequestProcessor.Should().BeOfType<LoanRequest>();
        }

        [Fact]
        public void GetRequestProcessorInstance_PaymentType()
        {
            var requestProcessorFactory = new RequestProcessorFactory(_dataRepository.Object, _response.Object);

            var objRequestProcessor = requestProcessorFactory.GetRequestProcessorInstance(Util.RequestType.Payment);
            objRequestProcessor.Should().NotBeNull();
            objRequestProcessor.Should().BeOfType<PaymentRequest>();
        }

        [Fact]
        public void GetRequestProcessorInstance_BalanceType()
        {
            var requestProcessorFactory = new RequestProcessorFactory(_dataRepository.Object, _response.Object);

            var objRequestProcessor = requestProcessorFactory.GetRequestProcessorInstance(Util.RequestType.Balance);
            objRequestProcessor.Should().NotBeNull();
            objRequestProcessor.Should().BeOfType<BalanceRequest>();
        }
    }
}