namespace PhotoBooth.Models
{
    public abstract class ItemBaseDTO : DTOBase
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string DescriptionHtml { get; set; }

    }
}