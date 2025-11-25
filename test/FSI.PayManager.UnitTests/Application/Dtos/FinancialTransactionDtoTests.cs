using FSI.PayManager.Application.Dtos;
using FluentAssertions;
using Xunit;
using System;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class FinancialTransactionDtoTests
    {
        [Fact]
        public void FinancialTransactionDto_Should_Implement_IHasId()
        {
            // Arrange
            var dto = new FinancialTransactionDto();

            // Assert
            dto.Should().BeAssignableTo<IHasId>();
        }

        [Fact]
        public void Properties_Should_Be_Gettable_And_Settable()
        {
            // Arrange
            var dueDate = new DateTime(2025, 1, 10);
            var paidDate = new DateTime(2025, 1, 12);
            var createdAt = DateTime.UtcNow.AddDays(-1);
            var updatedAt = DateTime.UtcNow;

            var dto = new FinancialTransactionDto
            {
                Id = 10,
                UserId = 99,
                WalletId = 5,
                CategoryId = 3,
                Title = "Electricity Bill",
                Description = "January consumption",
                Amount = 150.75m,
                TransactionType = "Expense",
                DueDate = dueDate,
                PaidDate = paidDate,
                Status = "Paid",
                IsRecurring = true,
                RecurringTransactionId = 77,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            dto.Id.Should().Be(10);
            dto.UserId.Should().Be(99);
            dto.WalletId.Should().Be(5);
            dto.CategoryId.Should().Be(3);
            dto.Title.Should().Be("Electricity Bill");
            dto.Description.Should().Be("January consumption");
            dto.Amount.Should().Be(150.75m);
            dto.TransactionType.Should().Be("Expense");
            dto.DueDate.Should().Be(dueDate);
            dto.PaidDate.Should().Be(paidDate);
            dto.Status.Should().Be("Paid");
            dto.IsRecurring.Should().BeTrue();
            dto.RecurringTransactionId.Should().Be(77);
            dto.CreatedAt.Should().Be(createdAt);
            dto.UpdatedAt.Should().Be(updatedAt);
        }

        [Fact]
        public void Nullable_Properties_Should_Accept_Null()
        {
            // Arrange
            var dto = new FinancialTransactionDto
            {
                Description = null,
                DueDate = null,
                PaidDate = null,
                UpdatedAt = null,
                RecurringTransactionId = null
            };

            // Assert
            dto.Description.Should().BeNull();
            dto.DueDate.Should().BeNull();
            dto.PaidDate.Should().BeNull();
            dto.UpdatedAt.Should().BeNull();
            dto.RecurringTransactionId.Should().BeNull();
        }

        [Fact]
        public void Default_Bool_Values_Should_Be_False()
        {
            // Arrange
            var dto = new FinancialTransactionDto();

            // Assert
            dto.IsRecurring.Should().BeFalse();
        }

        [Fact]
        public void Amount_Should_Support_Decimal_Values()
        {
            // Arrange
            var dto = new FinancialTransactionDto
            {
                Amount = 1234.56m
            };

            // Assert
            dto.Amount.Should().Be(1234.56m);
        }
    }
}