using FSI.PayManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class ReminderTests
    {
        [Fact]
        public void Constructor_Should_Set_All_Properties_And_Defaults()
        {
            // Arrange
            int userId = 1;
            int financialTransactionId = 10;
            int daysBeforeDue = 5;

            // Act
            var reminder = new Reminder(userId, financialTransactionId, daysBeforeDue);

            // Assert
            reminder.UserId.Should().Be(userId);
            reminder.FinancialTransactionId.Should().Be(financialTransactionId);
            reminder.DaysBeforeDue.Should().Be(daysBeforeDue);

            reminder.IsSent.Should().BeFalse();
            reminder.SentAt.Should().BeNull();

            reminder.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void MarkAsSent_Should_Set_IsSent_And_SentAt()
        {
            // Arrange
            var reminder = new Reminder(userId: 1, financialTransactionId: 2, daysBeforeDue: 3);
            DateTime sentAt = new DateTime(2025, 01, 10, 8, 30, 0);

            // Act
            reminder.MarkAsSent(sentAt);

            // Assert
            reminder.IsSent.Should().BeTrue();
            reminder.SentAt.Should().Be(sentAt);
        }

        [Fact]
        public void Reminder_Should_Inherit_From_BaseEntity()
        {
            // Arrange & Act
            var reminder = new Reminder(
                userId: 1,
                financialTransactionId: 10,
                daysBeforeDue: 3);

            // Assert
            reminder.Should().BeAssignableTo<BaseEntity>();
        }

        [Fact]
        public void MarkAsSent_Should_Not_Change_CreatedAt()
        {
            // Arrange
            var reminder = new Reminder(1, 2, 3);
            var createdAtBefore = reminder.CreatedAt;
            DateTime sentAt = DateTime.UtcNow.AddMinutes(1);

            // Act
            reminder.MarkAsSent(sentAt);

            // Assert
            reminder.CreatedAt.Should().Be(createdAtBefore);
        }
    }
}