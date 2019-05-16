using System;

namespace Spatem.Core.Entities
{
    public class BaseEntity : IEquatable<BaseEntity>
    {
        public Guid Id { get; protected set; }

        public BaseEntity(Guid? id)
        {
            Id = id ?? Guid.NewGuid();
        }

        public bool Equals(BaseEntity other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}