using EagleBank.Models.DTO.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Implementation;
using EagleBank.Orchestration.Tests.helpers;
using EagleBank.Repository.Interfaces;
using FluentAssertions;
using Moq;

namespace EagleBank.Orchestration.Tests
{
    public class TransactionOrchestratorTests
    {
        [Fact]
        public async Task TransactionOrchestrator_CreateTransaction_Returns_Success()
        {
            var transAccountRepoMock = new Mock<ITransactionRepository>();

            transAccountRepoMock.Setup(repo => repo.CreateTransactionAsync(It.IsAny<CreateTransactionRequestDTO>(), TestHelpers.mockUsername)).ReturnsAsync(TestHelpers.TransactionResponseDTO());

            var orchestrator = new TransactionOrchestrator(transAccountRepoMock.Object);

            var result = await orchestrator.CreateTransactionAsync(TestHelpers.CreateTransactionRequest(),TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<TransactionResponse>();
        }
        [Fact]
        public async Task TransactionOrchestrator_GetTransactionByTransactionId_Returns_Success()
        {
            var transAccountRepoMock = new Mock<ITransactionRepository>();

            transAccountRepoMock.Setup(repo => repo.GetTransactionByTransactionIdAsync(It.IsAny<string>(), TestHelpers.mockUsername,TestHelpers.mockAccountNumber)).ReturnsAsync(TestHelpers.TransactionResponseDTO());

            var orchestrator = new TransactionOrchestrator(transAccountRepoMock.Object);

            var result = await orchestrator.GetTransactionByTransactionIdAsync("tr1-132",TestHelpers.mockUsername,TestHelpers.mockAccountNumber);

            result.Should().NotBeNull();
            result.Should().BeOfType<TransactionResponse>();
        }
        [Fact]
        public async Task TransactionOrchestrator_GetTransactionsByAccountNumber_Returns_Success()
        {
            var transAccountRepoMock = new Mock<ITransactionRepository>();

            transAccountRepoMock.Setup(repo => repo.GetTransactionsAsync(It.IsAny<string>(), TestHelpers.mockUsername)).ReturnsAsync(TestHelpers.TransactionsResponseDTO());

            var orchestrator = new TransactionOrchestrator(transAccountRepoMock.Object);

            var result = await orchestrator.GetTransactionsByAccountNumberAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<TransactionsResponse>();
        }
        
    }
}
