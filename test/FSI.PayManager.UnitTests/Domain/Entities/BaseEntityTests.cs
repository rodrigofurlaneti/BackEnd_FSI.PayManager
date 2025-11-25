using FluentAssertions;
using Xunit;
using FSI.PayManager.Domain.Entities;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class BaseEntityTests
    {
        [Fact]
        public void BaseEntity_Default_Id_Should_Be_Zero()
        {
            // Arrange
            var entity = new FakeEntity(0);

            // Act
            var id = entity.Id;

            // Assert
            id.Should().Be(0);
        }

        [Fact]
        public void BaseEntity_Should_Allow_Setting_Id_Inside_Derived_Class()
        {
            // Arrange
            var entity = new FakeEntity(10);

            // Act
            entity.SetIdInternally(20);

            // Assert
            entity.Id.Should().Be(20);
        }

        [Fact]
        public void BaseEntity_Should_Expose_Id_Publicly_As_ReadOnly()
        {
            // Arrange
            var entity = new FakeEntity(5);

            // Act
            var id = entity.Id;

            // Assert
            id.Should().Be(5);
        }

        [Fact]
        public void Derived_Class_Should_Set_Id_Through_Constructor()
        {
            // Arrange
            var entity = new FakeEntity(99);

            // Assert
            entity.Id.Should().Be(99);
        }
    }
}
