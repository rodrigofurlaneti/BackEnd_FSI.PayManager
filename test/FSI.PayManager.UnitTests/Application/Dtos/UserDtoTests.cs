using System;
using FluentAssertions;
using Xunit;
using FSI.PayManager.Application.Dtos;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class UserDtoTests
    {
        [Fact]
        public void UserDto_Should_Implement_IHasId()
        {
            // Arrange
            var dto = new UserDto();

            // Assert
            dto.Should().BeAssignableTo<IHasId>();
        }

        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var created = new DateTime(2025, 1, 10);

            var dto = new UserDto
            {
                Id = 1,
                FullName = "Rodrigo Furlaneti",
                Email = "rodrigo@example.com",
                PasswordHash = "HASH123",
                CreatedAt = created,
                IsActive = true
            };

            // Assert
            dto.Id.Should().Be(1);
            dto.FullName.Should().Be("Rodrigo Furlaneti");
            dto.Email.Should().Be("rodrigo@example.com");
            dto.PasswordHash.Should().Be("HASH123");
            dto.CreatedAt.Should().Be(created);
            dto.IsActive.Should().BeTrue();
        }

        [Fact]
        public void FullName_And_Email_And_PasswordHash_Should_Not_Be_Null_After_Assignment()
        {
            // Arrange
            var dto = new UserDto
            {
                FullName = "Teste",
                Email = "email@test.com",
                PasswordHash = "xxhash"
            };

            // Assert
            dto.FullName.Should().Be("Teste");
            dto.Email.Should().Be("email@test.com");
            dto.PasswordHash.Should().Be("xxhash");
        }

        [Fact]
        public void IsActive_Should_Default_To_False()
        {
            // Arrange
            var dto = new UserDto();

            // Assert
            dto.IsActive.Should().BeFalse();
        }

        [Fact]
        public void CreatedAt_Should_Store_Correct_Value()
        {
            // Arrange
            var date = new DateTime(2024, 12, 31);

            var dto = new UserDto
            {
                CreatedAt = date
            };

            // Assert
            dto.CreatedAt.Should().Be(date);
        }
    }
}
