using System.Collections.Generic;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;

namespace PhotoBooth.BL.Facades
{
    public class ItemPackage
    {
        public string Name { get; set; }
        public ICollection<RentalItemModel> RentalItems { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}