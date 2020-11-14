using System;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.DAL.Entity
{
    public abstract class EntityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
