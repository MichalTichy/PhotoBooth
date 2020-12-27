using System;
using System.ComponentModel.DataAnnotations;
namespace PhotoBooth.DAL.Entity
{
    public abstract class ItemBase : IEntity
    {
        public Guid Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string PictureUrl { get; set; }
        [MaxLength(250)]
        public string DescriptionHtml { get; set; }

    }
}