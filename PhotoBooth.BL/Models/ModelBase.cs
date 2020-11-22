using System;
using Riganti.Utils.Infrastructure.Core;
namespace PhotoBooth.BL.Models
{
    public abstract class ModelBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
