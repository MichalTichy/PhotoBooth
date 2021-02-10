using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;

namespace UnitTests
{
    public class TestUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider<PhotoBoothContext>
    {
        public TestUnitOfWorkProvider() : base(new AsyncLocalUnitOfWorkRegistry(), DbContextFactory)
        {
        }

        private static PhotoBoothContext DbContextFactory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PhotoBoothContext>();

            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.EnableSensitiveDataLogging();

            return new PhotoBoothContext(optionsBuilder.Options);
        }
    }
}