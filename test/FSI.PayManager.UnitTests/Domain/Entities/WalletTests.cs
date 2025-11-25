using FSI.PayManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class WalletTests
    {
        [Fact]
        public void Constructor_Should_Set_All_Properties_Correctly()
        {
            // Arrange
            int userId = 1;
            string name = "Main Wallet";
            string? description = "Primary account";
            decimal initialBalance = 1500.75m;
            bool isDefault = true;

            // Act
            var wallet = new Wallet(userId, name, description, initialBalance, isDefault);

            // Assert
            wallet.UserId.Should().Be(userId);
            wallet.Name.Should().Be(name);
            wallet.Description.Should().Be(description);
            wallet.InitialBalance.Should().Be(initialBalance);
            wallet.IsDefault.Should().Be(isDefault);
            wallet.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));

            wallet.Transactions.Should().NotBeNull();
            wallet.Transactions.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_Should_Set_IsDefault_To_False_When_NotProvided()
        {
            // Arrange
            int userId = 2;
            string name = "Savings";
            string? description = null;
            decimal initialBalance = 500;

            // Act
            var wallet = new Wallet(userId, name, description, initialBalance);

            // Assert
            wallet.IsDefault.Should().BeFalse();
        }

        [Fact]
        public void Update_Should_Modify_All_Editable_Properties()
        {
            // Arrange
            var wallet = new Wallet(
                userId: 1,
                name: "Old Name",
                description: "Old Desc",
                initialBalance: 100,
                isDefault: false);

            string newName = "Updated Name";
            string? newDesc = "Updated Desc";
            decimal newBalance = 999.99m;
            bool newDefault = true;

            // Act
            wallet.Update(newName, newDesc, newBalance, newDefault);

            // Assert
            wallet.Name.Should().Be(newName);
            wallet.Description.Should().Be(newDesc);
            wallet.InitialBalance.Should().Be(newBalance);
            wallet.IsDefault.Should().Be(newDefault);
        }

        [Fact]
        public void Transactions_Should_Initialize_As_Empty_List()
        {
            // Arrange
            var wallet = new Wallet(1, "Test", null, 0);

            // Act & Assert
            wallet.Transactions.Should().NotBeNull();
            wallet.Transactions.Should().BeEmpty();
        }

        [Fact]
        public void Wallet_Should_Inherit_From_BaseEntity()
        {
            // Arrange & Act
            var wallet = new Wallet(1, "Test", null, 0);

            // Assert
            wallet.Should().BeAssignableTo<BaseEntity>();
        }
    }
}