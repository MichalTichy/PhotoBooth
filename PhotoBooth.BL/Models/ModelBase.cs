using System;
using Riganti.Utils.Infrastructure.Core;
namespace PhotoBooth.BL.Models
{
    public abstract class ModelBase : IModel
    {
        public Guid Id { get; set; }
    }
}
