using EagleBank.Models.APIException;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Entities;
using EagleBank.Repository.Entities;
using EagleBank.Repository.Implementation;
using EagleBank.Repository.Service;
using EagleBank.Repository.Tests.helpers;
using FluentAssertions;
using Moq;

namespace EagleBank.Repository.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task AddUserDetailsAsync_Returns_Success()
        {

            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<UserEntity>())).Returns(Task.CompletedTask);

            var repository = new UserRepository(cacheService.Object);

            var result = await repository.AddUserDetailsAsync(TestHelpers.UserDetailsRequestDTO());

            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailsResponseDTO>();

            cacheService.Verify(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<UserEntity>()), Times.Once);
        }
        [Fact]
        public async Task GetUserDetailsAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.GetFromCacheAsync<UserEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.UserEntity()));

            var repository = new UserRepository(cacheService.Object);

            var result = await repository.GetUserDetailsAsync(TestHelpers.mockUsername);

            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailsResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<UserEntity>(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task GetUserDetailsAsync_UserNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new UserRepository(cacheService.Object);


            var action = async () => await repository.GetUserDetailsAsync(TestHelpers.mockUsername);

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("User was not found");

        }
        [Fact]
        public async Task DeleteUserDetailsAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            cacheService.Setup(cs => cs.RemoveFromCacheAsync(It.IsAny<string>())).ReturnsAsync(true);


            var repository = new UserRepository(cacheService.Object);

            var result = await repository.DeleteUserDetailsAsync(TestHelpers.mockUsername);

            result.Should().BeTrue();

            cacheService.Verify(cs => cs.RemoveFromCacheAsync(It.IsAny<string>()), Times.Once);
        }
        [Fact]
        public async Task UpdateUserDetailsAsync_Returns_Success()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new UserRepository(cacheService.Object);

            cacheService.Setup(cs => cs.GetFromCacheAsync<UserEntity>(It.IsAny<string>()))
                .ReturnsAsync((true, TestHelpers.UserEntity()));


            cacheService.Setup(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<UserEntity>())).Returns(Task.CompletedTask);


            var result  = await repository.UpdateUserDetailsAsync(TestHelpers.UserDetailsRequestDTO());
            result.Should().NotBeNull();
            result.Should().BeOfType<UserDetailsResponseDTO>();

            cacheService.Verify(cs => cs.GetFromCacheAsync<UserEntity>(It.IsAny<string>()), Times.Once);

            cacheService.Verify(cs => cs.WriteToCacheAsync(It.IsAny<string>(), It.IsAny<UserEntity>()), Times.Once);

        }
        [Fact]
        public async Task UpdateUserDetailsAsync_UserNotFound_Throws()
        {
            var cacheService = new Mock<ICacheService>();

            var repository = new UserRepository(cacheService.Object);


            var action = async () => await repository.UpdateUserDetailsAsync(TestHelpers.UserDetailsRequestDTO());

            await action.Should().ThrowAsync<NotFoundErrorException>()
                 .WithMessage("User was not found");

        }

    }
}
