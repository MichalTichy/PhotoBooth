using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.DAL.Entity
{
    public class ItemPackage : EntityBase
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<RentalItem> RentalItems { get; set; }
        public ICollection<Product> Products { get; set; }
        public bool CurrentlyAvailable { get; set; }
    }
}