using FSI.PayManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class RecurringTransactionTests
    {
        [Fact]
        public void Constructor_Should_Set_All_Properties_And_Set_Defaults()
        {
            // Arrange
            int userId = 10;
            int walletId = 20;
            int categoryId = 30;
            string title = "Gym Membership";
            string description = "Monthly renewal";
            decimal amount = 99.90m;
            string transactionType = "Expense";
            string frequency = "Monthly";
            byte? dayOfMonth = 5;
            byte? dayOfWeek = null;
            DateTime startDate = new DateTime(2025, 1, 1);
            DateTime? endDate = new DateTime(2025, 12, 31);
            DateTime nextOccurrence = new DateTime(2025, 2, 5);

            // Act
            var recurring = new RecurringTransaction(
                userId,
                walletId,
                categoryId,
                title,
                description,
                amount,
                transactionType,
                frequency,
                dayOfMonth,
                dayOfWeek,
                startDate,
                endDate,
                nextOccurrence);

            // Assert
            recurring.UserId.Should().Be(userId);
            recurring.WalletId.Should().Be(walletId);
            recurring.CategoryId.Should().Be(categoryId);
            recurring.Title.Should().Be(title);
            recurring.Description.Should().Be(description);
            recurring.Amount.Should().Be(amount);
            recurring.TransactionType.Should().Be(transactionType);
            recurring.Frequency.Should().Be(frequency);
            recurring.DayOfMonth.Should().Be(dayOfMonth);
            recurring.DayOfWeek.Should().Be(dayOfWeek);
            recurring.StartDate.Should().Be(startDate);
            recurring.EndDate.Should().Be(endDate);
            recurring.NextOccurrenceDate.Should().Be(nextOccurrence);

            recurring.IsActive.Should().BeTrue();
            recurring.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
            recurring.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void Update_Should_Update_All_Editable_Properties_And_Set_UpdatedAt()
        {
            // Arrange
            var recurring = new RecurringTransaction(
                userId: 10,
                walletId: 20,
                categoryId: 30,
                title: "Old Title",
                description: "Old Desc",
                amount: 20.50m,
                transactionType: "Expense",
                frequency: "Monthly",
                dayOfMonth: 1,
                dayOfWeek: null,
                startDate: new DateTime(2025, 1, 1),
                endDate: new DateTime(2025, 12, 31),
                nextOccurrenceDate: new DateTime(2025, 2, 1));

            // New values
            string newTitle = "Updated Title";
            string? newDesc = "Updated Description";
            decimal newAmount = 75.10m;
            string newType = "Income";
            string newFrequency = "Weekly";
            byte? newDayOfMonth = null;
            byte? newDayOfWeek = 3;
            DateTime newStartDate = new DateTime(2026, 1, 1);
            DateTime? newEndDate = new DateTime(2026, 12, 31);
            DateTime newNextOccurrence = new DateTime(2026, 1, 7);
            bool newIsActive = false;

            // Act
            recurring.Update(
                newTitle,
                newDesc,
                newAmount,
                newType,
                newFrequency,
                newDayOfMonth,
                newDayOfWeek,
                newStartDate,
                newEndDate,
                newNextOccurrence,
                newIsActive);

            // Assert
            recurring.Title.Should().Be(newTitle);
            recurring.Description.Should().Be(newDesc);
            recurring.Amount.Should().Be(newAmount);
            recurring.TransactionType.Should().Be(newType);
            recurring.Frequency.Should().Be(newFrequency);
            recurring.DayOfMonth.Should().Be(newDayOfMonth);
            recurring.DayOfWeek.Should().Be(newDayOfWeek);
            recurring.StartDate.Should().Be(newStartDate);
            recurring.EndDate.Should().Be(newEndDate);
            recurring.NextOccurrenceDate.Should().Be(newNextOccurrence);
            recurring.IsActive.Should().BeFalse();

            recurring.UpdatedAt.Should().NotBeNull();
            recurring.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void RecurringTransaction_Should_Inherit_From_BaseEntity()
        {
            // Arrange & Act
            var recurring = new RecurringTransaction(
                userId: 1,
                walletId: 2,
                categoryId: 3,
                title: "Test",
                description: null,
                amount: 10,
                transactionType: "Expense",
                frequency: "Monthly",
                dayOfMonth: 1,
                dayOfWeek: null,
                startDate: DateTime.UtcNow,
                endDate: null,
                nextOccurrenceDate: DateTime.UtcNow.AddDays(30));

            // Assert
            recurring.Should().BeAssignableTo<BaseEntity>();
        }
    }
}