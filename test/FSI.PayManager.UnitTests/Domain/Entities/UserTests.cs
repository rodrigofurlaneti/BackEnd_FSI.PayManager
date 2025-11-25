using FSI.PayManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class UserTests
    {
        [Fact]
        public void Constructor_Should_Set_Properties_And_Defaults()
        {
            // Arrange
            string fullName = "John Doe";
            string email = "john@example.com";
            string passwordHash = "HASH123";

            // Act
            var user = new User(fullName, email, passwordHash);

            // Assert
            user.FullName.Should().Be(fullName);
            user.Email.Should().Be(email);
            user.PasswordHash.Should().Be(passwordHash);

            user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
            user.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Wallets_Should_Be_Initialized_As_Empty_List()
        {
            // Arrange
            var user = new User("Test User", "test@example.com", "HASH");

            // Act & Assert
            user.Wallets.Should().NotBeNull();
            user.Wallets.Should().BeEmpty();
        }

        [Fact]
        public void Update_Should_Modify_FullName_And_Email()
        {
            // Arrange
            var user = new User("Old Name", "old@example.com", "HASH");

            string newName = "New Name";
            string newEmail = "new@example.com";

            // Act
            user.Update(newName, newEmail);

            // Assert
            user.FullName.Should().Be(newName);
            user.Email.Should().Be(newEmail);
        }

        [Fact]
        public void Deactivate_Should_Set_IsActive_False()
        {
            // Arrange
            var user = new User("User", "user@example.com", "HASH");

            // Act
            user.Deactivate();

            // Assert
            user.IsActive.Should().BeFalse();
        }

        [Fact]
        public void Activate_Should_Set_IsActive_True()
        {
            // Arrange
            var user = new User("User", "user@example.com", "HASH");

            // desativa primeiro
            user.Deactivate();
            user.IsActive.Should().BeFalse();

            // Act
            user.Activate();

            // Assert
            user.IsActive.Should().BeTrue();
        }

        [Fact]
        public void User_Should_Inherit_From_BaseEntity()
        {
            // Arrange & Act
            var user = new User("User", "u@example.com", "HASH");

            // Assert
            user.Should().BeAssignableTo<BaseEntity>();
        }
    }
}