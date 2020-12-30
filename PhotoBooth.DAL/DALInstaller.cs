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
                return () =>
                {
                    var s = provider.CreateScope();
                    var dContext = s.ServiceProvider.GetService<PhotoBoothContext>();
                    return dContext;
                };
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
