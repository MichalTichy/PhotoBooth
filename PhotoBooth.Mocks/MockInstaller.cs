using Microsoft.Extensions.DependencyInjection;
using PhotoBooth.BL.Facades;

namespace PhotoBooth.Mocks
{
    public static class MockInstaller
    {
        public static void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOrderFacade, OrderFacadeMock>();
            serviceCollection.AddSingleton<IProductFacade, ProductFacadeMock>();
            serviceCollection.AddSingleton<ICatalogFacade,CatalogFacadeMock>();
        }
    }
}