using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.DAL.Entity
{
    public class ItemPackage : EntityBase
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ItemPackageRentalItem> RentalItems { get; set; }
        public virtual ICollection<ItemPackageProduct> Products { get; set; }
        public bool CurrentlyAvailable { get; set; }
    }

    public class ItemPackageRentalItem
    {
        public Guid ItemPackageId { get; set; }
        public virtual ItemPackage ItemPackage { get; set; }

        public RentalItemType RentalItemType { get; set; }
    }

    public class ItemPackageProduct
    {
        public Guid ItemPackageId { get; set; }
        public virtual ItemPackage ItemPackage { get; set; }
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}