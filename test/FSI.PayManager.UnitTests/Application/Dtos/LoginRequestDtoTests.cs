using FSI.PayManager.Application.Dtos;
using FluentAssertions;
using Xunit;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class LoginRequestDtoTests
    {
        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                Email = "test@example.com",
                Password = "MySecret123"
            };

            // Assert
            dto.Email.Should().Be("test@example.com");
            dto.Password.Should().Be("MySecret123");
        }

        [Fact]
        public void Email_Should_Not_Be_Null_After_Assignment()
        {
            // Arrange
            var dto = new LoginRequestDto();

            // Act
            dto.Email = "user@test.com";

            // Assert
            dto.Email.Should().NotBeNull();
            dto.Email.Should().Be("user@test.com");
        }

        [Fact]
        public void Password_Should_Not_Be_Null_After_Assignment()
        {
            // Arrange
            var dto = new LoginRequestDto();

            // Act
            dto.Password = "pass123";

            // Assert
            dto.Password.Should().NotBeNull();
            dto.Password.Should().Be("pass123");
        }

        [Fact]
        public void Properties_Should_Accept_Empty_Strings()
        {
            // Arrange
            var dto = new LoginRequestDto
            {
                Email = "",
                Password = ""
            };

            // Assert
            dto.Email.Should().BeEmpty();
            dto.Password.Should().BeEmpty();
        }
    }
}