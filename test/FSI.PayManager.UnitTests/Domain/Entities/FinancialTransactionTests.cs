using FSI.PayManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class FinancialTransactionTests
    {
        [Fact]
        public void Constructor_Should_Set_All_Properties_Correctly_And_Set_CreatedAt()
        {
            // Arrange
            int userId = 1;
            int walletId = 2;
            int categoryId = 3;
            string title = "Electricity Bill";
            string description = "January payment";
            decimal amount = 150.75m;
            string transactionType = "Expense";
            DateTime? dueDate = new DateTime(2025, 01, 10);
            string status = "Pending";
            bool isRecurring = false;
            int? recurringId = null;

            // Act
            var transaction = new FinancialTransaction(
                userId,
                walletId,
                categoryId,
                title,
                description,
                amount,
                transactionType,
                dueDate,
                status,
                isRecurring,
                recurringId);

            // Assert
            transaction.UserId.Should().Be(userId);
            transaction.WalletId.Should().Be(walletId);
            transaction.CategoryId.Should().Be(categoryId);
            transaction.Title.Should().Be(title);
            transaction.Description.Should().Be(description);
            transaction.Amount.Should().Be(amount);
            transaction.TransactionType.Should().Be(transactionType);
            transaction.DueDate.Should().Be(dueDate);
            transaction.Status.Should().Be(status);
            transaction.IsRecurring.Should().Be(isRecurring);
            transaction.RecurringTransactionId.Should().BeNull();

            transaction.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
            transaction.PaidDate.Should().BeNull();
            transaction.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void MarkPaid_Should_Set_PaidDate_Status_And_UpdatedAt()
        {
            // Arrange
            var transaction = new FinancialTransaction(
                userId: 1,
                walletId: 2,
                categoryId: 3,
                title: "Gym Payment",
                description: "Monthly membership",
                amount: 99.90m,
                transactionType: "Expense",
                dueDate: DateTime.UtcNow.Date,
                status: "Pending",
                isRecurring: false,
                recurringTransactionId: null);

            DateTime paidDate = new DateTime(2025, 01, 05);

            // Act
            transaction.MarkPaid(paidDate);

            // Assert
            transaction.PaidDate.Should().Be(paidDate);
            transaction.Status.Should().Be("Paid");
            transaction.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void Update_Should_Modify_All_Editable_Properties_And_Set_UpdatedAt()
        {
            // Arrange
            var transaction = new FinancialTransaction(
                userId: 1,
                walletId: 10,
                categoryId: 20,
                title: "Old Title",
                description: "Old Desc",
                amount: 10m,
                transactionType: "Expense",
                dueDate: new DateTime(2025, 01, 01),
                status: "Pending",
                isRecurring: false,
                recurringTransactionId: null);

            // New values
            string newTitle = "New Title";
            string? newDesc = "Updated Desc";
            decimal newAmount = 55.99m;
            string newType = "Income";
            DateTime? newDueDate = new DateTime(2025, 05, 20);
            DateTime? newPaidDate = new DateTime(2025, 06, 01);
            string newStatus = "Paid";
            bool newIsRecurring = true;
            int? newRecurringId = 999;

            // Act
            transaction.Update(
                newTitle,
                newDesc,
                newAmount,
                newType,
                newDueDate,
                newPaidDate,
                newStatus,
                newIsRecurring,
                newRecurringId);

            // Assert
            transaction.Title.Should().Be(newTitle);
            transaction.Description.Should().Be(newDesc);
            transaction.Amount.Should().Be(newAmount);
            transaction.TransactionType.Should().Be(newType);
            transaction.DueDate.Should().Be(newDueDate);
            transaction.PaidDate.Should().Be(newPaidDate);
            transaction.Status.Should().Be(newStatus);
            transaction.IsRecurring.Should().Be(newIsRecurring);
            transaction.RecurringTransactionId.Should().Be(newRecurringId);

            transaction.UpdatedAt.Should().NotBeNull();
            transaction.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void FinancialTransaction_Should_Inherit_From_BaseEntity()
        {
            // Arrange & Act
            var transaction = new FinancialTransaction(
                userId: 1,
                walletId: 2,
                categoryId: 3,
                title: "Inheritance Test",
                description: null,
                amount: 10,
                transactionType: "Expense",
                dueDate: null,
                status: "Pending",
                isRecurring: false,
                recurringTransactionId: null);

            // Assert
            transaction.Should().BeAssignableTo<BaseEntity>();
        }
    }
}