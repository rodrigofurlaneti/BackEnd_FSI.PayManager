using System;
using FSI.PayManager.Application.Dtos;
using FluentAssertions;
using Xunit;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class LoginResponseDtoTests
    {
        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var expires = new DateTime(2025, 1, 10, 12, 0, 0, DateTimeKind.Utc);

            var dto = new LoginResponseDto
            {
                AccessToken = "fake-jwt-token",
                ExpiresAtUtc = expires,
                UserId = 42,
                FullName = "John Doe",
                Email = "john.doe@example.com"
            };

            // Assert
            dto.AccessToken.Should().Be("fake-jwt-token");
            dto.ExpiresAtUtc.Should().Be(expires);
            dto.UserId.Should().Be(42);
            dto.FullName.Should().Be("John Doe");
            dto.Email.Should().Be("john.doe@example.com");
        }

        [Fact]
        public void AccessToken_Should_Not_Be_Null_After_Assignment()
        {
            // Arrange
            var dto = new LoginResponseDto();

            // Act
            dto.AccessToken = "token-123";

            // Assert
            dto.AccessToken.Should().NotBeNull();
            dto.AccessToken.Should().Be("token-123");
        }

        [Fact]
        public void ExpiresAtUtc_Should_Store_Utc_DateTime()
        {
            // Arrange
            var utcDate = DateTime.UtcNow;
            var dto = new LoginResponseDto
            {
                ExpiresAtUtc = utcDate
            };

            // Assert
            dto.ExpiresAtUtc.Should().Be(utcDate);
        }

        [Fact]
        public void FullName_And_Email_Should_Not_Be_Null_After_Assignment()
        {
            // Arrange
            var dto = new LoginResponseDto
            {
                FullName = "Jane Doe",
                Email = "jane@example.com"
            };

            // Assert
            dto.FullName.Should().Be("Jane Doe");
            dto.Email.Should().Be("jane@example.com");
        }
    }
}