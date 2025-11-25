using System;
using FluentAssertions;
using Xunit;
using FSI.PayManager.Application.Dtos;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class WalletDtoTests
    {
        [Fact]
        public void WalletDto_Should_Implement_IHasId()
        {
            // Arrange
            var dto = new WalletDto();

            // Assert
            dto.Should().BeAssignableTo<IHasId>();
        }

        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var created = new DateTime(2025, 1, 10);

            var dto = new WalletDto
            {
                Id = 1,
                UserId = 99,
                Name = "Main Wallet",
                Description = "Used for daily expenses",
                InitialBalance = 500.75m,
                IsDefault = true,
                CreatedAt = created
            };

            // Assert
            dto.Id.Should().Be(1);
            dto.UserId.Should().Be(99);
            dto.Name.Should().Be("Main Wallet");
            dto.Description.Should().Be("Used for daily expenses");
            dto.InitialBalance.Should().Be(500.75m);
            dto.IsDefault.Should().BeTrue();
            dto.CreatedAt.Should().Be(created);
        }

        [Fact]
        public void Description_Should_Accept_Null()
        {
            // Arrange
            var dto = new WalletDto
            {
                Description = null
            };

            // Assert
            dto.Description.Should().BeNull();
        }

        [Fact]
        public void InitialBalance_Should_Support_Decimal()
        {
            // Arrange
            var dto = new WalletDto
            {
                InitialBalance = 1234.56m
            };

            // Assert
            dto.InitialBalance.Should().Be(1234.56m);
        }

        [Fact]
        public void IsDefault_Should_Default_To_False()
        {
            // Arrange
            var dto = new WalletDto();

            // Assert
            dto.IsDefault.Should().BeFalse();
        }

        [Fact]
        public void CreatedAt_Should_Store_Correct_Value()
        {
            // Arrange
            var date = new DateTime(2024, 12, 31);

            var dto = new WalletDto
            {
                CreatedAt = date
            };

            // Assert
            dto.CreatedAt.Should().Be(date);
        }
    }
}