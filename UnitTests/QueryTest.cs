using NUnit.Framework;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class QueryTest
    {
        public string databaseStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IlinovTestDB011";

        [Test]
        public void TestInsert()
        {
            /* this code needs to be in first test as it creates database and fills it, and if it is in setup it does throws errors,
             * this cemented part of code mus be run only once
             * 
            using (var db = new PhotoBoothContext(databaseStr))
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
            using (var uow = new UnitOfWork(databaseStr))
            {
                for (int i = 0; i < 25; i++)
                {
                    uow.AddressRepository.Create(new Address() { City = "Kosice", PostalCode = i.ToString(), Street = "zvonicna " + i, BuildingNumber = "azp" + (i * i) });
                }
                uow.Save();
            }
            */



            string s = "";
            using (var uow = new UnitOfWork(databaseStr))
            {
                foreach (var address in uow.AddressRepository.Get())
                {
                    s = s + address.City + " " + address.PostalCode + " street: " + address.Street + " buildingNum: " + address.BuildingNumber + "\n";
                }
            }
            throw new Exception(s);
        }
        [Test]
        public void TestUpdate()
        {
            using (var db = new PhotoBoothContext(databaseStr))
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
            using (var uow = new UnitOfWork(databaseStr))
            {
                for (int i = 0; i < 25; i++)
                {
                    uow.AddressRepository.Create(new Address() { City = "Kosice", PostalCode = i.ToString(), Street = "zvonicna " + i, BuildingNumber = "azp" + (i * i) });
                }
                uow.Save();
            }
            string s = "";
            AddressQuery temp = new AddressQuery(databaseStr);
            foreach (var address in temp.ExecuteAsync()) 
            {
                s = s + address.City + " " + address.PostalCode + " street: " + address.Street + " buildingNum: " + address.BuildingNumber + "\n";
            }
            throw new Exception(s);
        }
      
    }
}
