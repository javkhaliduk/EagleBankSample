using EagleBank.Models.APIException;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Implementation;
using EagleBank.Orchestration.Tests.helpers;
using EagleBank.Repository.Interfaces;
using FluentAssertions;
using Moq;
using System;

namespace EagleBank.Orchestration.Tests
{
    public class UserDetailsOrchestratorTests
    {

        [Fact]
        public async Task UserDetailsOrchestrator_AddUserDetailsAsync_Returns_Success()
        {
            var userRepoMock = new Mock<IUserRepository>();

            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            userRepoMock.Setup(repo => repo.AddUserDetailsAsync(It.IsAny<UserDetailsRequestDTO>())).ReturnsAsync(TestHelpers.UserDetailsResponseDTO());

            var orchestrator = new UserDetailsOrchestrator(userRepoMock.Object, bankAccountRepoMock.Object);

            var result = await orchestrator.AddUserDetailsAsync(TestHelpers.UserDetailsRequest());

            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailsResponse>();
        }
        [Fact]
        public async Task UserDetailsOrchestrator_GetUserDetailsAsync_Returns_Success()
        {
            var userRepoMock = new Mock<IUserRepository>();
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();
            userRepoMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponseDTO());

            var orchestrator = new UserDetailsOrchestrator(userRepoMock.Object,bankAccountRepoMock.Object);

            var result = await orchestrator.GetUserDetailsAsync(TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailsResponse>();
        }
        [Fact]
        public async Task UserDetailsOrchestrator_UpdateUserDetailsAsync_Returns_Success()
        {
            var userRepoMock = new Mock<IUserRepository>();
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();

            userRepoMock.Setup(repo => repo.UpdateUserDetailsAsync(It.IsAny<UserDetailsRequestDTO>())).ReturnsAsync(TestHelpers.UserDetailsResponseDTO());

            userRepoMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponseDTO());

            var orchestrator = new UserDetailsOrchestrator(userRepoMock.Object, bankAccountRepoMock.Object);

            var result = await orchestrator.UpdateUserDetailsAsync(TestHelpers.UserDetailsRequest(),TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailsResponse>();
        }
        [Fact]
        public async Task UserDetailsOrchestrator_DeleteUserDetailsAsync_Returns_Success()
        {
            var userRepoMock = new Mock<IUserRepository>();
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();
            userRepoMock.Setup(repo => repo.DeleteUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var orchestrator = new UserDetailsOrchestrator(userRepoMock.Object, bankAccountRepoMock.Object);

            var result = await orchestrator.DeleteUserDetailsAsync(TestHelpers.mockUsername);

            result.Should().BeTrue();
        }
        [Fact]
        public async Task UserDetailsOrchestrator_DeleteUserDetailsAsync_IfAccountExists_Throws()
        {
            var userRepoMock = new Mock<IUserRepository>();
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();
            userRepoMock.Setup(repo => repo.DeleteUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(true);
            bankAccountRepoMock.Setup(repo => repo.GetAllBankAccountsAsync(It.IsAny<string>())).ReturnsAsync(new BankAccountsResponseDTO { });

            var orchestrator = new UserDetailsOrchestrator(userRepoMock.Object, bankAccountRepoMock.Object);

            var action = async ()=> await orchestrator.DeleteUserDetailsAsync(TestHelpers.mockUsername);

            await action.Should().ThrowAsync<ConflictErrorException>()
                .WithMessage("A user cannot be deleted when they are associated with a bank account");
        }
        [Fact]
        public async Task UserDetailsOrchestrator_DeleteUserDetailsAsync_Returns_False()
        {
            var userRepoMock = new Mock<IUserRepository>();
            var bankAccountRepoMock = new Mock<IBankAccountRepository>();
            userRepoMock.Setup(repo => repo.DeleteUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var orchestrator = new UserDetailsOrchestrator(userRepoMock.Object, bankAccountRepoMock.Object);

            var result = await orchestrator.DeleteUserDetailsAsync(TestHelpers.mockUsername);

            result.Should().BeFalse();
        }
    }
}
