using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using DbContextOptions = Riganti.Utils.Infrastructure.EntityFrameworkCore.DbContextOptions;

namespace UnitTests
{
    public class TestUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider<PhotoBoothContext>
    {

        private static string ServerName { get; set; } = Guid.NewGuid().ToString();
        public TestUnitOfWorkProvider() : base(new AsyncLocalUnitOfWorkRegistry(), DbContextFactory)
        {
            ServerName = null;
        }

        public TestUnitOfWorkProvider(string serverName) : base(new AsyncLocalUnitOfWorkRegistry(), DbContextFactory)
        {
            ServerName = serverName;
        }

        private static PhotoBoothContext DbContextFactory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PhotoBoothContext>();
            
            if (ServerName != null)
            {
                optionsBuilder.UseInMemoryDatabase(ServerName);
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
            optionsBuilder.EnableSensitiveDataLogging();

            return new PhotoBoothContext(optionsBuilder.Options);
        }
    }
}