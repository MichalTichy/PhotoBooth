using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private PhotoBoothContext context = new PhotoBoothContext();
        private BaseRepository<Address> addressRepo;
        private BaseRepository<ItemPackage> itemPackageRepo;
        private BaseRepository<Order> orderRepo;
        private BaseRepository<Product> productRepo;
        private BaseRepository<RentalItem> renatalItemRepo;

        public UnitOfWork(string database)
        {
            context = new PhotoBoothContext(database);
        }
        public UnitOfWork()
        {
            context = new PhotoBoothContext();
        }
        public BaseRepository<Address> AddressRepository 
        {
            get
            {
                if (addressRepo == null)
                {
                    this.addressRepo = new BaseRepository<Address>(context);
                }
                return addressRepo;
            }
        }
        public BaseRepository<ItemPackage> ItempackageRepository 
        {
            get
            {
                if (itemPackageRepo == null)
                {
                    this.itemPackageRepo = new BaseRepository<ItemPackage>(context);
                }
                return itemPackageRepo;
            }
        }
        public BaseRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepo == null)
                {
                    this.orderRepo = new BaseRepository<Order>(context);
                }
                return orderRepo;
            }
        }
        public BaseRepository<Product> ProductRepository
        {
            get
            {
                if (productRepo== null)
                {
                    this.productRepo = new BaseRepository<Product>(context);
                }
                return productRepo;
            }
        }
        public BaseRepository<RentalItem> RentalItemRepository
        {
            get
            {
                if (renatalItemRepo == null)
                {
                    this.renatalItemRepo = new BaseRepository<RentalItem>(context);
                }
                return renatalItemRepo;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
