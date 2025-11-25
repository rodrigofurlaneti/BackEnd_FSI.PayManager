using FluentAssertions;
using FSI.PayManager.Application.Dtos;
using FSI.PayManager.Application.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.UnitTests.Application.Interfaces
{
    public sealed class ICrudAppServiceTests
    {
        // DTO fake apenas para testar o contrato genérico
        private sealed class TestDto : IHasId
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }

        [Fact]
        public void ICrudAppService_Should_Be_Generic_With_IHasId_Constraint()
        {
            // Arrange
            var type = typeof(ICrudAppService<>);

            // Act
            var genericArgs = type.GetGenericArguments();

            // Assert
            genericArgs.Should().HaveCount(1);

            var constraint = genericArgs.Single()
                .GetGenericParameterConstraints()
                .SingleOrDefault();

            constraint.Should().NotBeNull();
            constraint!.Should().Be(typeof(IHasId));
        }

        [Fact]
        public void ICrudAppService_Should_Expose_All_Crud_Methods()
        {
            // Arrange
            var type = typeof(ICrudAppService<TestDto>);

            // Act
            var methods = type.GetMethods().Select(m => m.Name).ToList();

            // Assert
            methods.Should().Contain("GetAllAsync");
            methods.Should().Contain("GetByIdAsync");
            methods.Should().Contain("CreateAsync");
            methods.Should().Contain("UpdateAsync");
            methods.Should().Contain("DeleteAsync");
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_List_Of_Dto()
        {
            // Arrange
            var items = new List<TestDto>
            {
                new TestDto { Id = 1, Name = "Item 1" },
                new TestDto { Id = 2, Name = "Item 2" }
            };

            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(items);

            // Act
            var result = await mock.Object.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
            result[1].Name.Should().Be("Item 2");
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Dto_When_Found()
        {
            // Arrange
            var dto = new TestDto { Id = 10, Name = "Found" };

            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            // Act
            var result = await mock.Object.GetByIdAsync(10);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(10);
            result.Name.Should().Be("Found");
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((TestDto?)null);

            // Act
            var result = await mock.Object.GetByIdAsync(999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_Should_Return_Created_Dto()
        {
            // Arrange
            var input = new TestDto { Id = 0, Name = "New" };
            var created = new TestDto { Id = 1, Name = "New" };

            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.CreateAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(created);

            // Act
            var result = await mock.Object.CreateAsync(input);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("New");
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Updated_Dto_When_Found()
        {
            // Arrange
            var input = new TestDto { Id = 1, Name = "Updated" };
            var updated = new TestDto { Id = 1, Name = "Updated" };

            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.UpdateAsync(1, input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(updated);

            // Act
            var result = await mock.Object.UpdateAsync(1, input);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.Name.Should().Be("Updated");
        }

        [Fact]
        public async Task UpdateAsync_Should_Return_Null_When_Not_Found()
        {
            // Arrange
            var input = new TestDto { Id = 999, Name = "Does not exist" };

            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.UpdateAsync(999, input, It.IsAny<CancellationToken>()))
                .ReturnsAsync((TestDto?)null);

            // Act
            var result = await mock.Object.UpdateAsync(999, input);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Should_Complete_Without_Exception()
        {
            // Arrange
            var mock = new Mock<ICrudAppService<TestDto>>();
            mock.Setup(s => s.DeleteAsync(5, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var act = async () => await mock.Object.DeleteAsync(5);

            // Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Methods_Should_Respect_CancellationToken_When_Configured_To_Throw()
        {
            // Arrange
            var mock = new Mock<ICrudAppService<TestDto>>();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            mock.Setup(s => s.GetAllAsync(cts.Token))
                .ThrowsAsync(new TaskCanceledException());

            // Act
            var act = async () => await mock.Object.GetAllAsync(cts.Token);

            // Assert
            await act.Should().ThrowAsync<TaskCanceledException>();
        }
    }
}