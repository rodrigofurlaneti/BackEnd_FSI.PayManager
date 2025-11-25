using System;
using FluentAssertions;
using Xunit;
using FSI.PayManager.Application.Dtos;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class ReminderDtoTests
    {
        [Fact]
        public void ReminderDto_Should_Implement_IHasId()
        {
            // Arrange
            var dto = new ReminderDto();

            // Assert
            dto.Should().BeAssignableTo<IHasId>();
        }

        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var sentAtDate = new DateTime(2025, 1, 15);
            var created = new DateTime(2025, 1, 1);

            var dto = new ReminderDto
            {
                Id = 10,
                UserId = 99,
                FinancialTransactionId = 5,
                DaysBeforeDue = 3,
                IsSent = true,
                SentAt = sentAtDate,
                CreatedAt = created
            };

            // Assert
            dto.Id.Should().Be(10);
            dto.UserId.Should().Be(99);
            dto.FinancialTransactionId.Should().Be(5);
            dto.DaysBeforeDue.Should().Be(3);
            dto.IsSent.Should().BeTrue();
            dto.SentAt.Should().Be(sentAtDate);
            dto.CreatedAt.Should().Be(created);
        }

        [Fact]
        public void Nullable_Properties_Should_Accept_Null()
        {
            // Arrange
            var dto = new ReminderDto
            {
                SentAt = null
            };

            // Assert
            dto.SentAt.Should().BeNull();
        }

        [Fact]
        public void IsSent_Should_Default_To_False()
        {
            // Arrange
            var dto = new ReminderDto();

            // Assert
            dto.IsSent.Should().BeFalse();
        }

        [Fact]
        public void CreatedAt_Should_Store_Date_Correctly()
        {
            // Arrange
            var date = new DateTime(2024, 12, 31);

            var dto = new ReminderDto
            {
                CreatedAt = date
            };

            // Assert
            dto.CreatedAt.Should().Be(date);
        }
    }
}