using System;
using System.ComponentModel.DataAnnotations;
namespace PhotoBooth.DAL.Entity
{
    public class Address : EntityBase
    {
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(20)]
        public string PostalCode { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        [MaxLength(25)]
        public string BuildingNumber { get; set; }
        public override string ToString()
        {
            return " city:" + City + " postalCode: " + PostalCode + " street: " + Street + " buildingNum: " + BuildingNumber + " id: " + Id;
        }
    }
}