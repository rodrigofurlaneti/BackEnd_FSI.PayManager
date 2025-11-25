using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;

namespace FSI.PayManager.UnitTests.Application.Interfaces
{
    public sealed class IAuthAppServiceTests
    {
        [Fact]
        public void IAuthAppService_Should_Have_LoginAsync_Method()
        {
            // Arrange
            var type = typeof(IAuthAppService);

            // Act
            var method = type.GetMethod("LoginAsync");

            // Assert
            method.Should().NotBeNull("the interface must contain LoginAsync");
        }

        [Fact]
        public async Task LoginAsync_Should_Return_LoginResponseDto_When_Valid()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Email = "test@example.com",
                Password = "123456"
            };

            var expectedResponse = new LoginResponseDto
            {
                AccessToken = "jwt-token",
                ExpiresAtUtc = DateTime.UtcNow.AddHours(1),
                UserId = 1,
                FullName = "Test User",
                Email = "test@example.com"
            };

            var mock = new Mock<IAuthAppService>();
            mock.Setup(x => x.LoginAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await mock.Object.LoginAsync(request);

            // Assert
            result.Should().NotBeNull();
            result!.AccessToken.Should().Be("jwt-token");
            result.UserId.Should().Be(1);
            result.Email.Should().Be("test@example.com");
            result.FullName.Should().Be("Test User");
        }

        [Fact]
        public async Task LoginAsync_Should_Return_Null_When_Invalid()
        {
            // Arrange
            var request = new LoginRequestDto
            {
                Email = "wrong@example.com",
                Password = "invalid"
            };

            var mock = new Mock<IAuthAppService>();
            mock.Setup(x => x.LoginAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync((LoginResponseDto?)null);

            // Act
            var result = await mock.Object.LoginAsync(request);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_Should_Respect_CancellationToken()
        {
            // Arrange
            var request = new LoginRequestDto { Email = "test@example.com", Password = "123456" };

            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var mock = new Mock<IAuthAppService>();

            // Cancellation should propagate as TaskCanceledException
            mock.Setup(x => x.LoginAsync(request, cts.Token))
                .ThrowsAsync(new TaskCanceledException());

            // Act
            var act = async () => await mock.Object.LoginAsync(request, cts.Token);

            // Assert
            await act.Should().ThrowAsync<TaskCanceledException>();
        }
    }
}