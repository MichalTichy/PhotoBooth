using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoBooth.BL.Facades;
using PhotoBooth.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace PhotoBooth.BL
{
    public class BlInstaller
    {
        public static void Install(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddTransient(typeof(UserFacade));

            /*serviceCollection.Scan(selector =>
                selector.FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo(typeof(FacadeBase<>)))
                    .AsSelf()
                    .WithTransientLifetime());*/

            serviceCollection.Scan(selector =>
                selector.FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo(typeof(QueryBase<>)))
                    .AsSelf()
                    .WithTransientLifetime());
            
            serviceCollection.AddSingleton<IUnitOfWorkRegistry,AsyncLocalUnitOfWorkRegistry>();
            //serviceCollection.AddSingleton<Func<QUERYTYPE>>(x => () => x.GetService<QUERYTYPE>());
            
            serviceCollection.AddSingleton<IUnitOfWorkProvider, AppUnitOfWorkProvider>();
        }

    }
    public class AppUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider<PhotoBoothContext>
    {
        public AppUnitOfWorkProvider(
            IUnitOfWorkRegistry registry,
            Func<PhotoBoothContext> dbContextFactory)
            : base(registry, dbContextFactory)
        {
        }
    }
}
