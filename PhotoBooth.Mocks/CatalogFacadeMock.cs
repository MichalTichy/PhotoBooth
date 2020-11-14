using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.Mocks
{
    public class CatalogFacadeMock : ICatalogFacade
    {
        public ICollection<RentalItemModel> GetAvailableRentalItems(DateTime since, DateTime till, RentalItemType type)
        {
            return GenerateRentalItems();
        }

        public ICollection<ProductModel> GetAvailableProducts(DateTime since, DateTime till)
        {
            return GenerateProducts();
        }
        
        public bool AreAllRentalItemsAvailable(ICollection<RentalItemModel> items, DateTime since, DateTime till)
        {
            return true;
        }

        public ICollection<ItemPackage> GetAllPackages()
        {
            return GeneratePackages();
        }
        public static ICollection<ItemPackage> GeneratePackages()
        {
            var products = GenerateProducts();
            var rentalItems = GenerateRentalItems();
            return new List<ItemPackage>()
            {
                new ItemPackage()
                {
                    Name = "Balík 1",
                    Products = products.Take(1).ToList(),
                    RentalItems = new List<RentalItemModel>()
                    {
                        rentalItems.First(t=>t.Type==RentalItemType.Background),
                        rentalItems.First(t=>t.Type==RentalItemType.Employe),
                        rentalItems.First(t=>t.Type==RentalItemType.Prop),
                    }
                },

                new ItemPackage()
                {
                    Name = "Balík 2",
                    Products = products.Take(1).ToList(),
                    RentalItems = new List<RentalItemModel>()
                    {
                        rentalItems.Where(t=>t.Type==RentalItemType.Background).Skip(1).First(),
                        rentalItems.Where(t=>t.Type==RentalItemType.Employe).Skip(1).First(),
                        rentalItems.Where(t=>t.Type==RentalItemType.Prop).Skip(1).First(),
                    }
                }
            };
        }

        public static ICollection<RentalItemModel> GenerateRentalItems()
        {
            return new List<RentalItemModel>()
            {
                new RentalItemModel()
                {
                    Id = Guid.Parse("e5cd94b5-f418-4eec-a8b2-065bf57d9a31"),
                    PricePerHour = 100,
                    DescriptionHtml = "Desciption <b> bold <b/>",
                    Name = "Photo booth 1",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.PhotoBooth
                },

                new RentalItemModel()
                {
                    Id = Guid.Parse("484c723b-f5e1-493e-8617-60d7ffff1fca"),
                    PricePerHour = 900,
                    DescriptionHtml = "Desciption 2 <b> bold <b/>",
                    Name = "Photo booth 2",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.PhotoBooth
                },

                new RentalItemModel()
                {
                    Id = Guid.Parse("d2e370cf-9bc3-4b10-96b3-a1c71947d54c"),
                    PricePerHour = 1200,
                    DescriptionHtml = "Desciption 3 <b> bold <b/>",
                    Name = "Employe 1",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Employe
                },
                new RentalItemModel()
                {
                    Id = Guid.Parse("711ca7e8-bb8e-4bb9-a9f8-3d208925aee1"),
                    PricePerHour = 1000,
                    DescriptionHtml = "Desciption 3 <b> bold <b/>",
                    Name = "Employe 2",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Employe
                },
                new RentalItemModel()
                {
                    Id = Guid.Parse("711ca7e8-bb8e-4bb9-a9f8-3d208925aee1"),
                    PricePerHour = 200,
                    DescriptionHtml = "Desciption 3 <b> bold <b/>",
                    Name = "Background 1",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Background
                },
                new RentalItemModel()
                {
                    Id = Guid.Parse("99896886-ade6-4599-923a-411521191700"),
                    PricePerHour = 300,
                    DescriptionHtml = "Desciption 4 <b> bold <b/>",
                    Name = "Background 2",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Background
                },
                new RentalItemModel()
                {
                    Id = Guid.Parse("99896886-ade6-4599-923a-411521191700"),
                    PricePerHour = 150,
                    DescriptionHtml = "Desciption 5 <b> bold <b/>",
                    Name = "Prop 1",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Prop
                },
                new RentalItemModel()
                {
                    Id = Guid.Parse("99896886-ade6-4599-923a-411521191700"),
                    PricePerHour = 20,
                    DescriptionHtml = "Desciption 6 <b> bold <b/>",
                    Name = "Prop 2",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Prop
                },
                new RentalItemModel()
                {
                    Id = Guid.Parse("99896886-ade6-4599-923a-411521191700"),
                    PricePerHour = 50,
                    DescriptionHtml = "Desciption 7 <b> bold <b/>",
                    Name = "Prop 3",
                    PictureUrl = @"https://picsum.photos/200",
                    Type = RentalItemType.Prop
                }
            };
        }


        public static ICollection<ProductModel> GenerateProducts()
        {
            return new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = Guid.Parse("d7aedab8-350b-4ec6-9337-dab07fb98ee6"),
                    DescriptionHtml = "Product description <b> test </b>",
                    Name = "Product 1",
                    PictureUrl = @"https://picsum.photos/200",
                    Price = 130
                },

                new ProductModel()
                {
                    Id = Guid.Parse("d7aedab8-350b-4ec6-9337-dab07fb98ee6"),
                    DescriptionHtml = "Product 2 description <b> test </b>",
                    Name = "Product 2",
                    PictureUrl = @"https://picsum.photos/200",
                    Price = 10
                },

                new ProductModel()
                {
                    Id = Guid.Parse("d7aedab8-350b-4ec6-9337-dab07fb98ee6"),
                    DescriptionHtml = "Product 3 description <b> test </b>",
                    Name = "Product 3",
                    PictureUrl = @"https://picsum.photos/200",
                    Price = 133
                }
            };
        }
    }
}
