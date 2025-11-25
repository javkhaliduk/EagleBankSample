using EagleBank.Models.DTO.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Implementation;
using EagleBank.Orchestration.Tests.helpers;
using EagleBank.Repository.Interfaces;
using FluentAssertions;
using Moq;

namespace EagleBank.Orchestration.Tests
{
    public class BankAccountOrchestratorTests
    {
        [Fact]
        public async Task BankAccountOrchestrator_AddUserDetailsAsync_Returns_Success()
        {
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            bankAccountRepoMock.Setup(repo => repo.AddBankAccountAsync(It.IsAny<BankAccountRequestDTO>(),TestHelpers.mockUsername)).ReturnsAsync(TestHelpers.BankAccountResponseDTO());

            var orchestrator = new BankAccountOrchestrator(bankAccountRepoMock.Object);

            var result = await orchestrator.CreateBankAccountAsync(TestHelpers.CreateBankAccountRequest(),TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<CreateBankAccountResponse>();
        }
        [Fact]
        public async Task BankAccountOrchestrator_UpdateBankAccountAsync_Returns_Success()
        {
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            bankAccountRepoMock.Setup(repo => repo.UpdateBankAccountAsync(It.IsAny<BankAccountRequestDTO>(),TestHelpers.mockAccountNumber, TestHelpers.mockUsername)).ReturnsAsync(TestHelpers.BankAccountResponseDTO());


            var orchestrator = new BankAccountOrchestrator(bankAccountRepoMock.Object);

            var result = await orchestrator.UpdateBankAccountAsync(TestHelpers.PatchBankAccountRequest(),TestHelpers.mockAccountNumber,TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<PatchBankAccountResponse>();
        }
        [Fact]
        public async Task BankAccountOrchestrator_GetByAccountNumberAsync_Returns_Success()
        {
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            bankAccountRepoMock.Setup(repo => repo.GetBankAccountAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername)).ReturnsAsync(TestHelpers.BankAccountResponseDTO());


            var orchestrator = new BankAccountOrchestrator(bankAccountRepoMock.Object);

            var result = await orchestrator.GetByAccountNumberAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<BankAccountResponse>();
        }
        [Fact]
        public async Task BankAccountOrchestrator_DeleteByAccountNumberAsync_Returns_Success()
        {
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            bankAccountRepoMock.Setup(repo => repo.DeleteBankAccountAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername)).ReturnsAsync(true);


            var orchestrator = new BankAccountOrchestrator(bankAccountRepoMock.Object);

            var result = await orchestrator.DeleteByAccountNumberAsync(TestHelpers.mockAccountNumber,TestHelpers.mockUsername);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task BankAccountOrchestrator_GetAllBankAccountsAsync_Returns_False()
        {
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            bankAccountRepoMock.Setup(repo => repo.GetAllBankAccountsAsync(TestHelpers.mockUsername)).ReturnsAsync(TestHelpers.BankAccountsResponseDTO());


            var orchestrator = new BankAccountOrchestrator(bankAccountRepoMock.Object);

            var result = await orchestrator.GetAllBankAccountsAsync(TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<BankAccountsResponse>();
        }
    }
}
