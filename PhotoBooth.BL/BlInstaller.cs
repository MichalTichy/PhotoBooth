using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PhotoBooth.BL.Facades;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.BL
{
    public class BlInstaller
    {
        public static void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(UserFacade));
            //serviceCollection.AddTransient<IOrderFacade,)>();
        }
    }
}
