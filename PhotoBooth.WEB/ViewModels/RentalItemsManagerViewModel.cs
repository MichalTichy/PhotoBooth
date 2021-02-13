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
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.WEB.ViewModels
{
    [Authorize(Roles = "admin")]
    public class RentalItemsManagerViewModel : MasterPageViewModel
    {
        private readonly ICatalogFacade _catalogFacade;

        public GridViewDataSet<RentalItemModel> RentalItems { get; set; } = new GridViewDataSet<RentalItemModel>() { RowEditOptions = { PrimaryKeyPropertyName = "Id" } };
        public RentalItemModel NewItem { get; set; } = new RentalItemModel() { Name = "new name", PricePerHour = 20, Type=RentalItemType.PhotoBooth, DescriptionHtml = "desc", PictureUrl = "https://nieco" };

        public RentalItemsManagerViewModel(ICatalogFacade catalogFacade)
        {
            this._catalogFacade = catalogFacade;
        }

        public async override Task PreRender()
        {
            var t1 = _catalogFacade.GetAllRentalItemsAsync();
            await base.PreRender();

            RentalItems.LoadFromQueryable((await t1).AsQueryable());
        }

        public void Edit(RentalItemModel product)
        {
            RentalItems.RowEditOptions.EditRowId = product.Id;
        }

        public async void Update(RentalItemModel rentalItem)
        {
            RentalItems.RowEditOptions.EditRowId = null;
            await _catalogFacade.UpdateRentalItemAsync(rentalItem);
            RentalItems.RequestRefresh();
        }

        public async void AddNew()
        {
            await _catalogFacade.CreateRentalItemAsync(NewItem);
            RentalItems.RequestRefresh();
        }

        public async void Delete(RentalItemModel rentalItem)
        {
            await _catalogFacade.DeleteRentalItemAsync(rentalItem);
            RentalItems.RequestRefresh();
        }

        public void CancelEdit()
        {
            RentalItems.RowEditOptions.EditRowId = null;

            // Refresh GridView items
            RentalItems.RequestRefresh();
        }
    }
}

