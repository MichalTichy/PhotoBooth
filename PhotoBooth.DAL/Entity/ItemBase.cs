namespace PhotoBooth.DAL.Entity
{
    public abstract class ItemBase : EntityBase
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string DescriptionHtml { get; set; }

    }
}