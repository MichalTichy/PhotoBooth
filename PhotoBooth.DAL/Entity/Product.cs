namespace PhotoBooth.DAL.Entity
{
    public class Product : ItemBase
    {
        public double Price { get; set; }
        public uint AmountLeft { get; set; }
    }
}