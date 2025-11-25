using FluentAssertions;
using Xunit;
using FSI.PayManager.Domain.Entities;

namespace FSI.PayManager.UnitTests.Domain.Entities
{
    public class FakeEntity : BaseEntity
    {
        public FakeEntity(int id)
        {
            Id = id;
        }

        public void SetIdInternally(int id)
        {
            Id = id;
        }
    }
}
