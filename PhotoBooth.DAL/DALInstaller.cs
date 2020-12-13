using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.DAL
{
    public class DALInstaller
    {
        public static void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<Func<PhotoBoothContext>>(provider =>
            {
                var s = provider.CreateScope();
                return s.ServiceProvider.GetService<PhotoBoothContext>;
            }); 
            serviceCollection.AddSingleton<IDateTimeProvider, LocalDateTimeProvider>();
            serviceCollection.Scan(selector =>
                selector.FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}
