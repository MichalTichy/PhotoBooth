using System;

namespace PhotoBooth.BL.Models
{
    public abstract class ModelBase : IModel
    {
        public Guid Id { get; set; }
    }
}