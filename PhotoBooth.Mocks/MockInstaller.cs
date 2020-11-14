using Microsoft.Extensions.DependencyInjection;
using PhotoBooth.BL.Facades;

namespace PhotoBooth.Mocks
{
    public static class MockInstaller
    {
        public static void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IOrderFacade, OrderFacadeMock>();
            serviceCollection.AddTransient<ICatalogFacade,CatalogFacadeMock>();
        }
    }
}