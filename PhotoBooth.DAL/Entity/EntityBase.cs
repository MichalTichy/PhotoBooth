using System;

namespace PhotoBooth.DAL.Entity
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}
