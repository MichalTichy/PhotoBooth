using System;
using System.Collections.Generic;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.BL.Facades
{
    public class ItemPackage
    {
        public string Name { get; set; }
        public ICollection<RentalItemType> RentalItemTypes { get; set; }
        public ICollection<Guid> ProductIds { get; set; }
    }
}