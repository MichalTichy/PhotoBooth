using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.ViewModel;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;

namespace PhotoBooth.WEB.ViewModels
{
    //[Authorize(Roles = "admin")]
    public class ProductsManagerViewModel : MasterPageViewModel
    {
        private readonly ICatalogFacade _catalogFacade;

        public GridViewDataSet<ProductModel> Products { get; set; } = new GridViewDataSet<ProductModel>() { RowEditOptions = { PrimaryKeyPropertyName = "Id" } };

        public ProductsManagerViewModel(ICatalogFacade catalogFacade)
        {
            this._catalogFacade = catalogFacade;
        }

        public async override Task PreRender()
        {
            var t1 = _catalogFacade.GetAllProductsAsync();
            await base.PreRender();
            
            Products.LoadFromQueryable((await t1).AsQueryable());
        }

        public void Edit(ProductModel product)
        {
            Products.RowEditOptions.EditRowId = product.Id;
        }

        public async void Update(ProductModel product)
        {
            Products.RowEditOptions.EditRowId = null;
            await _catalogFacade.UpdateProductAsync(product);
            Products.RequestRefresh();
        }

        public void CancelEdit()
        {
            Products.RowEditOptions.EditRowId = null;

            // Refresh GridView items
            Products.RequestRefresh();
        }

    }
}

