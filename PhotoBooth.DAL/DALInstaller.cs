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
            serviceCollection.Scan(selector =>
                selector.FromCallingAssembly()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
                    .AsSelfWithInterfaces()
                    .WithTransientLifetime());
        }
    }
}
