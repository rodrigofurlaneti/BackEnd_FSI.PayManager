using FluentAssertions;
using FSI.PayManager.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSI.PayManager.UnitTests.Application.Dtos
{
    public sealed class IHasIdTests
    {
        // Classe auxiliar apenas para testar o contrato da interface
        private sealed class TestHasId : IHasId
        {
            public int Id { get; set; }
        }

        [Fact]
        public void Id_Should_Be_ReadWrite()
        {
            // Arrange
            var entity = new TestHasId
            {
                Id = 10
            };

            // Assert inicial
            entity.Id.Should().Be(10);

            // Act
            entity.Id = 42;

            // Assert final
            entity.Id.Should().Be(42);
        }

        [Fact]
        public void Default_Id_Should_Be_Zero()
        {
            // Arrange
            var entity = new TestHasId();

            // Assert
            entity.Id.Should().Be(0);
        }
    }
}
