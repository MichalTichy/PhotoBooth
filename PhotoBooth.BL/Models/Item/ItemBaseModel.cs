namespace PhotoBooth.BL.Models.Item
{
    public abstract class ItemBaseModel : ModelBase
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string DescriptionHtml { get; set; }
    }
}