using System;

namespace PhotoBooth.DAL.Entity
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}