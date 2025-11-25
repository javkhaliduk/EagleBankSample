using EagleBank.Models.APIException;
using EagleBank.Models.DTO.Response;
using EagleBank.Repository.Entities;
using EagleBank.Repository.Implementation;
using EagleBank.Repository.Service;
using EagleBank.Repository.Tests.helpers;
using FluentAssertions;
using Moq;

namespace EagleBank.Repository.Tests
{
    public class BankAccountRepsitoryTests
    {
        [Fact]
        public async Task AddBankAccountAsync_Returns_Success()
        {

            var cacheService = new Mock<ICacheService>();


            cacheService.Setup(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<BankAccountsEntity>())).Returns(Task.CompletedTask);


            var repository = new BankAccountRepository(cacheService.Object);

            var result = await repository.AddBankAccountAsync(TestHelpers.BankAccountRequestDTO(), TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<BankAccountResponseDTO>();

            cacheService.Verify(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<BankAccountsEntity>()), Times.Once);
        }

        [Fact]
        public async Task GetBankAccountAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.BankAccountsEntity()));

            var repository = new BankAccountRepository(cacheService.Object);

            var result = await repository.GetBankAccountAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<BankAccountResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetBankAccountAsync_UserNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new BankAccountRepository(cacheService.Object);

            var action = async () => await repository.GetBankAccountAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("User was not found");

        }

        [Fact]
        public async Task GetBankAccountAsync_AccountNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.BankAccountsEntity()));

            var repository = new BankAccountRepository(cacheService.Object);

            var action = async () => await repository.GetBankAccountAsync("fake", TestHelpers.mockUsername);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("Account was not found");

            cacheService.Verify(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task GetAllBankAccountsAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.BankAccountsEntity()));

            var repository = new BankAccountRepository(cacheService.Object);

            var result = await repository.GetAllBankAccountsAsync(TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<BankAccountsResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetAllBankAccountsAsync_UserNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new BankAccountRepository(cacheService.Object);

            var action = async () => await repository.GetAllBankAccountsAsync(TestHelpers.mockUsername);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("User was not found");

        }
        [Fact]
        public async Task DeleteBankAccountAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.BankAccountsEntity()));


            cacheService.Setup(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<BankAccountsEntity>())).Returns(Task.CompletedTask);


            var repository = new BankAccountRepository(cacheService.Object);

            var result = await repository.DeleteBankAccountAsync(TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            result.Should().BeTrue();

            cacheService.Verify(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()), Times.Once);

            cacheService.Verify(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<BankAccountsEntity>()), Times.Once);
        }



        [Fact]
        public async Task UpdateBankAccountAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.BankAccountsEntity()));


            cacheService.Setup(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<BankAccountsEntity>())).Returns(Task.CompletedTask);


            var repository = new BankAccountRepository(cacheService.Object);

            var result = await repository.UpdateBankAccountAsync(TestHelpers.BankAccountRequestDTO(), TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<BankAccountResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()), Times.Once);

            cacheService.Verify(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<BankAccountsEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBankAccountAsync_UserNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new BankAccountRepository(cacheService.Object);

            var action = async () => await repository.UpdateBankAccountAsync(TestHelpers.BankAccountRequestDTO(), TestHelpers.mockAccountNumber, TestHelpers.mockUsername);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("User was not found");

        }

        [Fact]
        public async Task UpdateBankAccountAsync_AccountNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.BankAccountsEntity()));

            var repository = new BankAccountRepository(cacheService.Object);

            var action = async () => await repository.UpdateBankAccountAsync(TestHelpers.BankAccountRequestDTO(), "fake", TestHelpers.mockUsername);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("Account was not found");

            cacheService.Verify(cs => cs.GetFromCacheAsync<BankAccountsEntity>(It.IsAny<string>()), Times.Once);

        }
    }
}
