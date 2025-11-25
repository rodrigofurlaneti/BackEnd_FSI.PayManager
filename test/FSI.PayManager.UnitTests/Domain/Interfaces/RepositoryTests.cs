using System;
using System.Linq;
using System.Threading.Tasks;
using FSI.PayManager.Domain.Entities;
using FSI.PayManager.Domain.Interfaces;
using FluentAssertions;
using Xunit;

namespace FSI.PayManager.UnitTests.Domain.Interfaces
{
    public class RepositoryTests
    {
        private readonly IRepository<User> _repository;

        public RepositoryTests()
        {
            _repository = new InMemoryRepository<User>();
        }

        [Fact]
        public async Task AddAsync_Should_Assign_Id_And_Save_Entity()
        {
            // Arrange
            var user = new User("John Doe", "john@example.com", "HASH");

            // Act
            var created = await _repository.AddAsync(user);

            // Assert
            created.Id.Should().BeGreaterThan(0);
            var all = await _repository.GetAllAsync();
            all.Count.Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Entity_When_Exists()
        {
            // Arrange
            var user = await _repository.AddAsync(new User("A", "a@a.com", "HASH"));

            // Act
            var found = await _repository.GetByIdAsync(user.Id);

            // Assert
            found.Should().NotBeNull();
            found!.Email.Should().Be("a@a.com");
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_NotExists()
        {
            // Act
            var found = await _repository.GetByIdAsync(999);

            // Assert
            found.Should().BeNull();
        }

        [Fact]
        public async Task FindAsync_Should_Filter_Entities()
        {
            // Arrange
            await _repository.AddAsync(new User("Rodrigo", "r@x.com", "HASH"));
            await _repository.AddAsync(new User("Maria", "m@x.com", "HASH"));

            // Act
            var result = await _repository.FindAsync(u => u.Email.Contains("@x.com"));

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync_Should_Modify_Entity()
        {
            // Arrange
            var user = await _repository.AddAsync(new User("Old", "old@a.com", "HASH"));
            user.Update("New Name", "new@a.com");

            // Act
            var updated = await _repository.UpdateAsync(user);

            // Assert
            updated.FullName.Should().Be("New Name");

            var fromDb = await _repository.GetByIdAsync(updated.Id);
            fromDb!.Email.Should().Be("new@a.com");
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Entity()
        {
            // Arrange
            var user = await _repository.AddAsync(new User("X", "x@x.com", "HASH"));

            // Act
            await _repository.DeleteAsync(user.Id);

            // Assert
            var all = await _repository.GetAllAsync();
            all.Should().BeEmpty();
        }
    }
}