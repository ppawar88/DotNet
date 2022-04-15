using FluentAssertions;
using LedgerCo.RequestProcessor;
using LedgerCo.Util;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace LedgerCo.UnitTest.RequestProcessor
{
    public class FileParserTest
    {
        private FileParser _fileParser;
        public FileParserTest() => _fileParser = new FileParser();

        private string GetFolderPath()
        {
            var currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", string.Empty);
            var relativePath = Path.Combine(currentAssemblyPath, @"InputFile");
            return Path.GetFullPath(relativePath);
        }

        [Fact]
        public void GetInputCommands_WithInvalidFilePath()
        {
            string fileDirPath = GetFolderPath();
            string fileName = "InvalidInput.txt";

            var ex = Assert.Throws<FileNotFoundException>(() => _fileParser.GetInputCommands(Path.Combine(fileDirPath, fileName)));
            Assert.Equal("Input File not found", ex.Message);
        }

        [Fact]
        public void GetInputCommands_WithValidFilePath()
        {
            string fileDirPath = GetFolderPath();
            string fileName = "TestFile.txt";

            var lstCommands = _fileParser.GetInputCommands(Path.Combine(fileDirPath, fileName));
            lstCommands.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetRequestType_NullValue()
        {
            string command=null;

            var ex = Assert.Throws<Exception>(() => _fileParser.GetRequestType(command));
            Assert.Equal("Invalid Command", ex.Message);
        }

        [Fact]
        public void GetRequestType_EmptyCommand()
        {
            string command = string.Empty;

            var ex = Assert.Throws<Exception>(() => _fileParser.GetRequestType(command));
            Assert.Equal("Input Command Missing", ex.Message);
        }

        [Fact]
        public void GetRequestType_UnknowRequestTypeCommand()
        {
            string command = "DummyCmd IDIDI DALE 5000 5 5";

            var ex = Assert.Throws<Exception>(() => _fileParser.GetRequestType(command));
            Assert.Equal("Invalid Command", ex.Message);
        }

        [Fact]
        public void GetRequestType_Valid_LoanType()
        {
            string command = "LOAN IDIDI DALE 5000 5 5";

            var requestType = _fileParser.GetRequestType(command);
            requestType.Should().Be(RequestType.Loan);
        }

        [Fact]
        public void GetRequestType_Valid_PaymentType()
        {
            string command = "PAYMENT IDIDI DALE 500 5";

            var requestType = _fileParser.GetRequestType(command);
            requestType.Should().Be(RequestType.Payment);
        }

        [Fact]
        public void GetRequestType_Valid_BalanceType()
        {
            string command = "BALANCE IDIDI DALE 3";

            var requestType = _fileParser.GetRequestType(command);
            requestType.Should().Be(RequestType.Balance);
        }
    }
}
