using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;

namespace PhotoBooth.BL.Models
{
    public class ItemPackageDTO
    {
        public string Name { get; set; }
        public ICollection<RentalItemType> RentalItemTypes { get; set; }
        public ICollection<Guid> ProductIds { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                ItemPackageDTO p = (ItemPackageDTO)obj;
                return (Name == p.Name);
            }
        }
    }
}