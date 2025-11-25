using EagleBank.Controllers;
using EagleBank.Models.APIException;
using EagleBank.Models.common;
using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Tests.helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;

namespace EagleBank.Tests
{
    public class BankAccountControllerTests
    {
        [Fact]
        public async Task BankAccountController_Post_Returns_Success()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();


            bankAccountOrchMock.Setup(repo => repo.CreateBankAccountAsync(It.IsAny<CreateBankAccountRequest>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.CreateBankAccountResponse());
            userDetailsOrchestratorMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponse());

            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Post(new CreateBankAccountRequest { AccountType="Personal", Name= "My Account"});

            result.Should().NotBeNull();
            var createdResult = result as CreatedResult;
            createdResult.Should().NotBeNull();
            createdResult.Value.Should().BeOfType<CreateBankAccountResponse>();
        }

        [Fact]
        public async Task BankAccountController_Patch_Returns_Success()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();


            bankAccountOrchMock.Setup(repo => repo.UpdateBankAccountAsync(It.IsAny<PatchBankAccountRequest>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.PatchBankAccountResponse());
            
            userDetailsOrchestratorMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponse());

            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Patch(new PatchBankAccountRequest { AccountType = "Personal", Name = "My Account" },"01234567");

            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<PatchBankAccountResponse>();
        }
        [Fact]
        public async Task BankAccountController_Delete_Returns_Success()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();


            bankAccountOrchMock.Setup(repo => repo.DeleteByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);


            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Delete("01234567");

            result.Should().NotBeNull();
            var okResult = result as NoContentResult;
            okResult.Should().NotBeNull();
        }

        [Fact]
        public async Task BankAccountController_Get_Returns_Success()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();

            bankAccountOrchMock.Setup(repo => repo.GetAllBankAccountsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountsResponse());

            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get();

            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<BankAccountsResponse>();
        }
        [Fact]
        public async Task BankAccountController_Get_MissingToken_MissingToken_Throws()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();

            bankAccountOrchMock.Setup(repo => repo.GetAllBankAccountsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountsResponse());

            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            var action =async()=> await controller.Get();

            await action.Should().ThrowAsync<UnAuthorisedErrorException>()
               .WithMessage("Access token is missing or invalid");
        }

        [Fact]
        public async Task BankAccountController_GetByAccountNumber_Returns_Success()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();

            bankAccountOrchMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get("01234567");

            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<BankAccountResponse>();
        }
        [Fact]
        public async Task BankAccountController_GetByAccountNumber_EmptyAccountNumber_Throws()
        {
            var bankAccountOrchMock = new Mock<IBankAccountOrchestrator>();

            var userDetailsOrchestratorMock = new Mock<IUserDetailsOrchestrator>();

            bankAccountOrchMock.Setup(repo => repo.GetByAccountNumberAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestHelpers.BankAccountResponse());

            var controller = new BankAccountController(bankAccountOrchMock.Object, userDetailsOrchestratorMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get(null);

            var badRequestResult = result as ObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().Be("The request didn't supply all the necessary data");
        }

        
       
    }
}
