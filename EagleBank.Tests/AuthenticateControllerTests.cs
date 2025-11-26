using EagleBank.code;
using EagleBank.Controllers;
using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Tests.helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EagleBank.Tests
{
    public class AuthenticateControllerTests
    {
        [Fact]
        public void AuthenticateController_Post_Returns_Success()
        {
            var mockJWTGenerator= new Mock<IJWTGenerator>();

            var controller = new AuthenticateController(mockJWTGenerator.Object);

            var result = controller.Post(new AuthenticationRequest
            {
                Username = "testuser",
                Password = "password"
            });
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void AuthenticateController_Post_MissingDetails_Throws()
        {
            var mockJWTGenerator = new Mock<IJWTGenerator>();

            var controller = new AuthenticateController(mockJWTGenerator.Object);

            var action = ()=> controller.Post(new AuthenticationRequest
            {
                Username = string.Empty,
                Password = "password"
            });
            action.Should().Throw<BadRequestErrorException>()
               .WithMessage("The request didn't supply all the necessary data");
        }
    }
}
