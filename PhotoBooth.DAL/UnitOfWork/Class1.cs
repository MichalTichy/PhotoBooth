using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoBooth.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private BaseRepositoryWraper<Address> addressRepo;
        private BaseRepositoryWraper<ItemPackage> itemPackageRepo;
        private BaseRepositoryWraper<Order> orderRepo;
        private BaseRepositoryWraper<Product> productRepo;
        private BaseRepositoryWraper<RentalItem> renatalItemRepo;

        public UnitOfWork(string database)
        {
            var context = new PhotoBoothContext(database);
            addressRepo = new BaseRepositoryWraper<Address>(context);
            itemPackageRepo= new BaseRepositoryWraper<ItemPackage>(context);
            orderRepo = new BaseRepositoryWraper<Order>(context);
            productRepo = new BaseRepositoryWraper<Product>(context);
            renatalItemRepo = new BaseRepositoryWraper<RentalItem>(context);
        }
        public UnitOfWork()
        {
            context = new PhotoBoothContext();
        }
        public BaseRepository<Address> AddressRepository
        {
            get
            {
                if (this.addressRepo == null)
                {
                    this.addressRepo = new BaseRepository<Address>(context);
                }
                return addressRepo;
            }
        }
        public BaseRepository<ItemPackage> ItemPackageRepository
        {
            get
            {
                if (this.itemPackageRepo == null)
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
                if (this.orderRepo == null)
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
                if (this.productRepo == null)
                {
                    this.productRepo = new BaseRepository<Product>(context);
                }
                return productRepo;
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
