using FSI.PayManager.Application.Dtos;
using FluentAssertions;
using Xunit;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class CategoryDtoTests
    {
        [Fact]
        public void CategoryDto_Should_Implement_IHasId()
        {
            // Arrange
            var dto = new CategoryDto();

            // Assert
            dto.Should().BeAssignableTo<IHasId>();
        }

        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var dto = new CategoryDto
            {
                Id = 1,
                UserId = 99,
                Name = "Utilities",
                Type = "Expense",
                ColorHex = "#FF0000",
                IsSystem = true,
                IsActive = true
            };

            // Assert
            dto.Id.Should().Be(1);
            dto.UserId.Should().Be(99);
            dto.Name.Should().Be("Utilities");
            dto.Type.Should().Be("Expense");
            dto.ColorHex.Should().Be("#FF0000");
            dto.IsSystem.Should().BeTrue();
            dto.IsActive.Should().BeTrue();
        }

        [Fact]
        public void ColorHex_Should_Accept_Null()
        {
            // Arrange
            var dto = new CategoryDto
            {
                ColorHex = null
            };

            // Assert
            dto.ColorHex.Should().BeNull();
        }

        [Fact]
        public void Default_Bool_Values_Should_Be_False()
        {
            // Arrange
            var dto = new CategoryDto();

            // Assert
            dto.IsSystem.Should().BeFalse();
            dto.IsActive.Should().BeFalse();
        }
    }
}
