using EagleBank.Controllers;
using EagleBank.Models.APIException;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Tests.helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EagleBank.Tests
{

    public class UsersControllerTests
    {
        [Fact]
        public async Task UsersController_Get_Returns_Success()
        {
            var userOrchMock = new Mock<IUserDetailsOrchestrator>();


            userOrchMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponse());

            var controller = new UsersController(userOrchMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get(TestHelpers.mockUsername);

            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeOfType<UserDetailsResponse>();
        }

        [Fact]
        public async Task UsersController_Get_Returns_BadRequest_If_MissingUsername()
        {
            var userOrchMock = new Mock<IUserDetailsOrchestrator>();


            userOrchMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponse());

            var controller = new UsersController(userOrchMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get(null);

            result.Should().NotBeNull();
            var badRequestResult = result as ObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
            badRequestResult.Value.Should().Be("The request didn't supply all the necessary data");
        }
        [Fact]
        public async Task UsersController_Get_Returns_Unauthorised_If_MissingToken_()
        {
            var userOrchMock = new Mock<IUserDetailsOrchestrator>();


            userOrchMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponse());

            var controller = new UsersController(userOrchMock.Object);



            var action = async()=> await controller.Get(TestHelpers.mockUsername);
            await action.Should().ThrowAsync<UnAuthorisedErrorException>()
               .WithMessage("Access token is missing or invalid");
        }

        [Fact]
        public async Task UsersController_Get_Returns_Forbidden_IfAccessing_DifferentUserData()
        {
            var userOrchMock = new Mock<IUserDetailsOrchestrator>();


            userOrchMock.Setup(repo => repo.GetUserDetailsAsync(It.IsAny<string>())).ReturnsAsync(TestHelpers.UserDetailsResponse());

            var controller = new UsersController(userOrchMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = TestHelpers.GetClaimsPrincipal() }
            };

            var result = await controller.Get("123");

            result.Should().NotBeNull();
            var badRequestResult = result as ObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(403);
            badRequestResult.Value.Should().Be("This user is not allowed to access the transaction");
        }


    }
}
