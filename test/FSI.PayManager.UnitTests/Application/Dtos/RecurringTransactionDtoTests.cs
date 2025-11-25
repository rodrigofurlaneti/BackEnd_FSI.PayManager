using System;
using FluentAssertions;
using Xunit;
using FSI.PayManager.Application.Dtos;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class RecurringTransactionDtoTests
    {
        [Fact]
        public void RecurringTransactionDto_Should_Implement_IHasId()
        {
            // Arrange
            var dto = new RecurringTransactionDto();

            // Assert
            dto.Should().BeAssignableTo<IHasId>();
        }

        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);
            var nextOccurrence = new DateTime(2025, 2, 1);
            var created = DateTime.UtcNow.AddDays(-10);
            var updated = DateTime.UtcNow;

            var dto = new RecurringTransactionDto
            {
                Id = 1,
                UserId = 99,
                WalletId = 5,
                CategoryId = 3,
                Title = "Internet Bill",
                Description = "Monthly subscription",
                Amount = 99.99m,
                TransactionType = "Expense",
                Frequency = "Monthly",
                DayOfMonth = 15,
                DayOfWeek = null,
                StartDate = startDate,
                EndDate = endDate,
                NextOccurrenceDate = nextOccurrence,
                IsActive = true,
                CreatedAt = created,
                UpdatedAt = updated
            };

            // Assert
            dto.Id.Should().Be(1);
            dto.UserId.Should().Be(99);
            dto.WalletId.Should().Be(5);
            dto.CategoryId.Should().Be(3);
            dto.Title.Should().Be("Internet Bill");
            dto.Description.Should().Be("Monthly subscription");
            dto.Amount.Should().Be(99.99m);
            dto.TransactionType.Should().Be("Expense");
            dto.Frequency.Should().Be("Monthly");
            dto.DayOfMonth.Should().Be(15);
            dto.DayOfWeek.Should().BeNull();
            dto.StartDate.Should().Be(startDate);
            dto.EndDate.Should().Be(endDate);
            dto.NextOccurrenceDate.Should().Be(nextOccurrence);
            dto.IsActive.Should().BeTrue();
            dto.CreatedAt.Should().Be(created);
            dto.UpdatedAt.Should().Be(updated);
        }

        [Fact]
        public void Nullable_Properties_Should_Accept_Null()
        {
            // Arrange
            var dto = new RecurringTransactionDto
            {
                Description = null,
                EndDate = null,
                UpdatedAt = null,
                DayOfMonth = null,
                DayOfWeek = null
            };

            // Assert
            dto.Description.Should().BeNull();
            dto.EndDate.Should().BeNull();
            dto.UpdatedAt.Should().BeNull();
            dto.DayOfMonth.Should().BeNull();
            dto.DayOfWeek.Should().BeNull();
        }

        [Fact]
        public void Amount_Should_Support_Decimal()
        {
            // Arrange
            var dto = new RecurringTransactionDto
            {
                Amount = 1234.56m
            };

            // Assert
            dto.Amount.Should().Be(1234.56m);
        }

        [Fact]
        public void IsActive_Should_Default_To_False()
        {
            // Arrange
            var dto = new RecurringTransactionDto();

            // Assert
            dto.IsActive.Should().BeFalse();
        }

        [Fact]
        public void StartDate_And_NextOccurrence_Should_Store_Values_Correctly()
        {
            // Arrange
            var start = new DateTime(2024, 10, 1);
            var next = new DateTime(2024, 11, 1);

            var dto = new RecurringTransactionDto
            {
                StartDate = start,
                NextOccurrenceDate = next
            };

            // Assert
            dto.StartDate.Should().Be(start);
            dto.NextOccurrenceDate.Should().Be(next);
        }
    }
}