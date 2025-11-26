using EagleBank.Controllers;
using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Tests.helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EagleBank.Tests
{
    public class AccountTransactionsControllerTests
    {
        [Fact]
        public async Task AccountTransactionsController_Get_Returns_Success()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            accountOrchestratorMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            transactionOrchestratorMock.Setup(repo => repo.GetTransactionsByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.TransactionsResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };
            var result = await controller.Get("123456");
            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
        }
        [Fact]
        public async Task AccountTransactionsController_Get_MissingToken_Throws()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            accountOrchestratorMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            transactionOrchestratorMock.Setup(repo => repo.GetTransactionsByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.TransactionsResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);


            var action = async () => await controller.Get("123456");

            await action.Should().ThrowAsync<UnAuthorisedErrorException>()
                 .WithMessage("Access token is missing or invalid");
        }
        [Fact]
        public async Task AccountTransactionsController_Get_NoAccount_Throws()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            transactionOrchestratorMock.Setup(repo => repo.GetTransactionsByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.TransactionsResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var action = async () => await controller.Post("234567", It.IsAny<CreateTransactionRequest>());
            await action.Should().ThrowAsync<NotFoundErrorException>()
                .WithMessage("Bank account was not found");
        }
        [Fact]
        public async Task AccountTransactionsController_GetTransactionById_Returns_Success()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            transactionOrchestratorMock.Setup(repo => repo.GetTransactionByTransactionIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.TransactionResponse());

            accountOrchestratorMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get("123456", "123");
            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
        }
        [Fact]
        public async Task AccountTransactionsController_GetTransactionById_MissingToken_throws()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();


            transactionOrchestratorMock.Setup(repo => repo.GetTransactionByTransactionIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.TransactionResponse());

            accountOrchestratorMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);

            var action = async () => await controller.Get("123456", "123");
            await action.Should().ThrowAsync<UnAuthorisedErrorException>()
                .WithMessage("Access token is missing or invalid");
        }
        [Fact]
        public async Task AccountTransactionsController_GetTransactionById_NoAccount_Throws()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            transactionOrchestratorMock.Setup(repo => repo.GetTransactionByTransactionIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.TransactionResponse());



            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var action = async () => await controller.Post("234567", It.IsAny<CreateTransactionRequest>());
            await action.Should().ThrowAsync<NotFoundErrorException>()
                .WithMessage("Bank account was not found");
        }
        [Fact]
        public async Task AccountTransactionsController_PostTransaction_Returns_Success()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            accountOrchestratorMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };
            var result = await controller.Post("01234567", It.IsAny<CreateTransactionRequest>());

            result.Should().NotBeNull();
            var createdResult = result as CreatedResult;
            createdResult.Should().NotBeNull();
        }
        [Fact]
        public async Task AccountTransactionsController_PostTransaction_MissingToken_Throws()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            accountOrchestratorMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);


            var action = async () => await controller.Post("01234567", It.IsAny<CreateTransactionRequest>());

            await action.Should().ThrowAsync<UnAuthorisedErrorException>()
               .WithMessage("Access token is missing or invalid");
        }
        [Fact]
        public async Task AccountTransactionsController_PostTransaction_NoAccount_Throws()
        {
            var transactionOrchestratorMock = new Mock<ITransactionOrchestrator>();

            var accountOrchestratorMock = new Mock<IBankAccountOrchestrator>();

            var controller = new AccountTransactionsController(transactionOrchestratorMock.Object, accountOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var action = async () => await controller.Post("234567", It.IsAny<CreateTransactionRequest>());
            await action.Should().ThrowAsync<NotFoundErrorException>()
                .WithMessage("Bank account was not found");

        }


    }
}