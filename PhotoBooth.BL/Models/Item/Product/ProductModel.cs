using System.ComponentModel.DataAnnotations;
using PhotoBooth.BL.ValidationRules;

namespace PhotoBooth.BL.Models.Item.Product
{
    public class ProductModel : ItemBaseModel
    {
        public decimal Price { get; set; }
        public uint AmountLeft { get; set; }
    }
}