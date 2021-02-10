using Riganti.Utils.Infrastructure.Core;
using System;

namespace PhotoBooth.DAL.Entity
{
    public abstract class EntityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}