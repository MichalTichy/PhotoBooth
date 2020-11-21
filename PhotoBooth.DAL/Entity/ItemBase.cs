using System.ComponentModel.DataAnnotations;
namespace PhotoBooth.DAL.Entity
{
    public abstract class ItemBase : EntityBase
    {
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string PictureUrl { get; set; }
        [MaxLength(250)]
        public string DescriptionHtml { get; set; }

    }
}