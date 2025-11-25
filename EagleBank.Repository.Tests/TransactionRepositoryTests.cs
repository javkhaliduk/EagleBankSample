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
    public class TransactionRepositoryTests
    {
        [Fact]
        public async Task AddUserDetailsAsync_Returns_Success()
        {

            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()))
               .ReturnsAsync((true, TestHelpers.TransactionsEntity()));

            cacheService.Setup(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<TransactionsEntity>())).Returns(Task.CompletedTask);

            var repository = new TransactionRepository(cacheService.Object);



            var result = await repository.CreateTransaction(TestHelpers.CreateTransactionRequestDTO(), TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<TransactionResponseDTO>();

            cacheService.Verify(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<TransactionsEntity>()), Times.Once);
            cacheService.Verify(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetTransactionByTransactionId_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.TransactionsEntity()));

            var repository = new TransactionRepository(cacheService.Object);

            var result = await repository.GetTransactionByTransactionId(TestHelpers.mockTransactionId, TestHelpers.mockUsername, TestHelpers.mockAccountNumber);

            result.Should().NotBeNull();
            result.Should().BeOfType<TransactionResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetTransactionByTransactionId_TransactionNotFoundForAccount_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new TransactionRepository(cacheService.Object);


            var action = async () => await repository.GetTransactionByTransactionId("tr1-123", TestHelpers.mockUsername, TestHelpers.mockAccountNumber);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("No transactions found for the account");

        }
        [Fact]
        public async Task GetTransactionByTransactionId_TransactionNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new TransactionRepository(cacheService.Object);

            cacheService.Setup(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.TransactionsEntity()));


            var action = async () => await repository.GetTransactionByTransactionId("tr1-123", TestHelpers.mockUsername, TestHelpers.mockAccountNumber);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("Transaction not found");

        }
        [Fact]
        public async Task GetTransactions_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.TransactionsEntity()));

            var repository = new TransactionRepository(cacheService.Object);

            var result = await repository.GetTransactions(TestHelpers.mockUsername, TestHelpers.mockAccountNumber);

            result.Should().NotBeNull();
            result.Should().BeOfType<TransactionsResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<TransactionsEntity>(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetTransactions_TransactionsNotFoundForAccount_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new TransactionRepository(cacheService.Object);


            var action = async () => await repository.GetTransactions(TestHelpers.mockUsername, TestHelpers.mockAccountNumber);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("No transactions found for the user");

        }

    }
}
