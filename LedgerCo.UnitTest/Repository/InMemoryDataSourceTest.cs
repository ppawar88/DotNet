using FluentAssertions;
using LedgerCo.Models;
using LedgerCo.Repository;
using LedgerCo.UnitTest.MockData;
using System;
using System.Collections.Generic;
using Xunit;

namespace LedgerCo.UnitTest.Repository
{

    public class LoadInMemoryDataSource
    {
        public List<Loan> _loanLedgerDetail;
        IDataRepository _inMemoryDataSource;
        MockedData _mockedData;

        public LoadInMemoryDataSource()
        {
            _inMemoryDataSource = InMemoryDataSource.Instance;
            _mockedData = new MockedData();

            _loanLedgerDetail = _mockedData.GetDefaultLoanLedgerDetail();
            foreach (var loan in _loanLedgerDetail)
                _inMemoryDataSource.ProcessLoan(loan);
        }

    }

    public class InMemoryDataSourceTest : IClassFixture<LoadInMemoryDataSource>
    {
        IDataRepository _inMemoryDataSource;
        List<Loan> _loanLedgerDetail;
        MockedData _mockedData;
        Loan _invalidLoan;
        Balance _invalidBalance;

        public InMemoryDataSourceTest(LoadInMemoryDataSource loadInMemoryDataSource)
        {
            _inMemoryDataSource = InMemoryDataSource.Instance;
            _mockedData = new MockedData();
            _loanLedgerDetail = loadInMemoryDataSource._loanLedgerDetail;

            //UpdateDataSource();

            _invalidLoan = new Loan()
            {
                Bank = new Bank() { Name = "TEST" },
                Borrower = new Borrower() { Name = "CUST" },
                PrincipalAmount = 5000,
                LoanTenureInYear = 5,
                IntRate = 4
            };

            _invalidBalance = new Balance()
            {
                Bank = new Bank() { Name = "TEST" },
                Borrower = new Borrower() { Name = "CUST" },
                RepaymentAmount = 500,
                EMINumber = 5
            };
        }

        private void UpdateDataSource()
        {
            foreach (var loan in _loanLedgerDetail)
                _inMemoryDataSource.ProcessLoan(loan);
        }

        [Fact]
        public void GetBalance_With_InValidAccount()
        {
            var ex = Assert.Throws<Exception>(() => _inMemoryDataSource.GetBalance(_invalidBalance));
            Assert.Equal("Loan detail not found for balance check", ex.Message);
        }

        [Fact]
        public void ProcessPayment_With_InValidAccount()
        {
            var ex = Assert.Throws<Exception>(() => _inMemoryDataSource.ProcessPayment(_invalidLoan));
            Assert.Equal("Loan detail not found for payment", ex.Message);
        }

        [Fact]
        public void ProcessLoan_With_DuplicateAccount()
        {
            var loanAccount = _loanLedgerDetail[0];
            var ex = Assert.Throws<Exception>(() => _inMemoryDataSource.ProcessLoan(loanAccount));
            Assert.Equal("Duplicate loan entry", ex.Message);
        }

        [Fact]
        public void ProcessLoan_With_ValidAccount()
        {
            var moqLoan = new Loan
            {
                Bank = new Bank { Name = "IDIDI" },
                Borrower = new Borrower { Name = "PRAVIN" },
                PrincipalAmount = 6000,
                LoanTenureInYear = 4,
                IntRate = 2
            };
            var isSuccess = _inMemoryDataSource.ProcessLoan(moqLoan);
            Assert.True(isSuccess);
        }

        [Fact]
        public void ProcessPayment_With_ValidAccount()
        {
            var moqLoan = new Loan
            {
                Bank = new Bank { Name = "IDIDI" },
                Borrower = new Borrower { Name = "PRAVIN" },
                Payments = new List<Payment>() {
                    new Payment() { RepaymentAmount = 500, EMINumber = 6 }
                }
            };

            var isSuccess = _inMemoryDataSource.ProcessPayment(moqLoan);
            Assert.True(isSuccess);
        }

        [Fact]
        public void GetBalance_With_ValidAccount()
        {
            var balance = new Balance()
            {
                Bank = new Bank() { Name = "IDIDI" },
                Borrower = new Borrower() { Name = "Dale2" },
                EMINumber = 6
            };

            var updateBalance = _inMemoryDataSource.GetBalance(balance);
            updateBalance.Should().NotBeNull();
            updateBalance.Should().BeOfType(typeof(Balance));

            Assert.Equal(balance.Bank.Name, updateBalance.Bank.Name);
            Assert.Equal(balance.Borrower.Name, updateBalance.Borrower.Name);
            Assert.Equal(1100, updateBalance.RepaymentAmount);
            Assert.Equal(49, updateBalance.PendingEMICount);

        }
    }
}
