using FSI.PayManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace FSI.PayManager.Infrastructure.Persistence
{
    public sealed class PayManagerDbContext : DbContext
    {
        public PayManagerDbContext(DbContextOptions<PayManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<RecurringTransaction> RecurringTransactions => Set<RecurringTransaction>();
        public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();
        public DbSet<Reminder> Reminders => Set<Reminder>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.FullName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(255).IsRequired();
                entity.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();
                entity.HasIndex(x => x.Email).IsUnique();
            });

            // Wallets
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallets");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(255);
                entity.Property(x => x.InitialBalance).HasColumnType("decimal(18,2)");
                entity.Property(x => x.CreatedAt).IsRequired();
                entity.Property(x => x.IsDefault).IsRequired();

                entity.HasOne(x => x.User)
                    .WithMany(u => u.Wallets)
                    .HasForeignKey(x => x.UserId);
            });

            // Categories
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Type).HasMaxLength(20).IsRequired();
                entity.Property(x => x.ColorHex).HasMaxLength(7);
                entity.Property(x => x.IsSystem).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(x => x.UserId);
            });

            // RecurringTransactions
            modelBuilder.Entity<RecurringTransaction>(entity =>
            {
                entity.ToTable("RecurringTransactions");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).HasMaxLength(150).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.Amount).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TransactionType).HasMaxLength(20).IsRequired();
                entity.Property(x => x.Frequency).HasMaxLength(20).IsRequired();
                entity.Property(x => x.DayOfMonth);
                entity.Property(x => x.DayOfWeek);
                entity.Property(x => x.StartDate).IsRequired();
                entity.Property(x => x.NextOccurrenceDate).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Wallet)
                    .WithMany()
                    .HasForeignKey(x => x.WalletId);

                entity.HasOne(x => x.Category)
                    .WithMany()
                    .HasForeignKey(x => x.CategoryId);
            });

            // FinancialTransactions
            modelBuilder.Entity<FinancialTransaction>(entity =>
            {
                entity.ToTable("FinancialTransactions");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).HasMaxLength(150).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(500);
                entity.Property(x => x.Amount).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TransactionType).HasMaxLength(20).IsRequired();
                entity.Property(x => x.Status).HasMaxLength(20).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Wallet)
                    .WithMany(w => w.Transactions)
                    .HasForeignKey(x => x.WalletId);

                entity.HasOne(x => x.Category)
                    .WithMany()
                    .HasForeignKey(x => x.CategoryId);

                entity.HasOne(x => x.RecurringTransaction)
                    .WithMany(r => r.FinancialTransactions)
                    .HasForeignKey(x => x.RecurringTransactionId);
            });

            // Reminders
            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.ToTable("Reminders");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.DaysBeforeDue).IsRequired();
                entity.Property(x => x.IsSent).IsRequired();
                entity.Property(x => x.CreatedAt).IsRequired();

                entity.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.FinancialTransaction)
                    .WithMany(t => t.Reminders)
                    .HasForeignKey(x => x.FinancialTransactionId);
            });
        }
    }
}