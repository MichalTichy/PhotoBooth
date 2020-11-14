using System.ComponentModel.DataAnnotations;
using PhotoBooth.BL.ValidationRules;

namespace PhotoBooth.BL.Models.Item.Product
{
    public class ProductModel : ItemBaseModel
    {
        public double Price { get; set; }

    }
}