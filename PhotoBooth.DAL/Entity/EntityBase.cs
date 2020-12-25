using System;
using System.ComponentModel.DataAnnotations;
namespace PhotoBooth.DAL.Entity
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
