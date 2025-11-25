using FSI.PayManager.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void Constructor_Should_Set_All_Properties_Correctly_And_IsActive_Should_Be_True()
        {
            // Arrange
            var userId = 10;
            var name = "Groceries";
            var type = "Expense";
            var colorHex = "#FF0000";
            var isSystem = false;

            // Act
            var category = new Category(userId, name, type, colorHex, isSystem);

            // Assert
            category.UserId.Should().Be(userId);
            category.Name.Should().Be(name);
            category.Type.Should().Be(type);
            category.ColorHex.Should().Be(colorHex);
            category.IsSystem.Should().Be(isSystem);
            category.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Constructor_Should_Allow_IsSystem_Default_To_Be_False()
        {
            // Arrange
            var userId = 1;
            var name = "Salary";
            var type = "Income";
            string? colorHex = null;

            // Act
            var category = new Category(userId, name, type, colorHex);

            // Assert
            category.IsSystem.Should().BeFalse();
        }

        [Fact]
        public void Update_Should_Change_All_Editable_Properties()
        {
            // Arrange
            var category = new Category(
                userId: 1,
                name: "Old Name",
                type: "Expense",
                colorHex: "#000000",
                isSystem: false);

            var newName = "New Name";
            var newType = "Income";
            string? newColor = "#FFFFFF";
            var newIsSystem = true;
            var newIsActive = false;

            // Act
            category.Update(newName, newType, newColor, newIsSystem, newIsActive);

            // Assert
            category.Name.Should().Be(newName);
            category.Type.Should().Be(newType);
            category.ColorHex.Should().Be(newColor);
            category.IsSystem.Should().Be(newIsSystem);
            category.IsActive.Should().Be(newIsActive);
        }

        [Fact]
        public void Update_Should_Allow_Reactivating_Category()
        {
            // Arrange
            var category = new Category(
                userId: 1,
                name: "Streaming",
                type: "Expense",
                colorHex: "#00FF00",
                isSystem: false);

            // desativa
            category.Update(category.Name, category.Type, category.ColorHex, category.IsSystem, isActive: false);
            category.IsActive.Should().BeFalse("the category was deactivated");

            // Act - reativa
            category.Update(category.Name, category.Type, category.ColorHex, category.IsSystem, isActive: true);

            // Assert
            category.IsActive.Should().BeTrue("the category should be reactivated");
        }

        [Fact]
        public void Category_Should_Inherit_From_BaseEntity()
        {
            // Arrange & Act
            var category = new Category(
                userId: 1,
                name: "Transport",
                type: "Expense",
                colorHex: "#123456");

            // Assert
            category.Should().BeAssignableTo<BaseEntity>();
        }
    }
}