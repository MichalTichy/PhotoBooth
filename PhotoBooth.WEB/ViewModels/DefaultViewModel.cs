using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models;

namespace PhotoBooth.WEB.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly ICatalogFacade _catalogFacade;
        private readonly IOrderFacade _orderFacade;

        public DefaultViewModel(ICatalogFacade catalogFacade,IOrderFacade orderFacade)
        {
            _catalogFacade = catalogFacade;
            _orderFacade = orderFacade;
        }

        //order stages
        public bool OrderMetadataForm { get; set; } = true;
        public bool ServiceSelect { get; set; } = false;
        public bool BoothSelect { get; set; } = false;
        public bool BackgroundSelect { get; set; } = false;
        public bool PropsSelect { get; set; } = false;
        public bool Summary { get; set; } = false;

        [Required]
        public OrderMatadata OrderBasicInfo { get; set; }
        public ICollection<ItemPackageDTO> Packages { get; set; }
        public bool CustomPackage => SelectedPackage == null;
        public ICollection<ProductModel> Products { get; set; }
        public ICollection<RentalItemType> SelectedRentalItemTypes { get; set; } =new List<RentalItemType>();
        public ICollection<Guid> SelectedProductIds { get; set; } =new List<Guid>();
        public ICollection<RentalItemType> AvailableRentalTypes { get; set; } = Enum.GetValues(typeof(RentalItemType)).Cast<RentalItemType>().ToList();        

        public ItemPackageDTO SelectedPackage { get; set; }
        public override Task PreRender()
        {
            if (OrderMetadataForm)
            {
                LoadDataOrderMetadataFrom();
            }

            if (ServiceSelect)
            {
                //LoadDataForServiceSelect();
            }

            if (BoothSelect)
            {
                LoadDataForBoothSelect();
            }

            if (BackgroundSelect)
            {
                LoadDataForBackgroundSelect();
            }

            if (PropsSelect)
            {
                LoadDataForPropsSelect();
            }

            if (Summary)
            {
                LoadDataForSummary();
            }
            return base.PreRender();
        }

        private void LoadDataForPropsSelect()
        {
            Props = _catalogFacade.GetAvailableRentalItems(OrderBasicInfo.Since, OrderBasicInfo.Till, RentalItemType.Prop);

        }

        private void LoadDataForBackgroundSelect()
        {
            Backgrounds= _catalogFacade.GetAvailableRentalItems(OrderBasicInfo.Since, OrderBasicInfo.Till, RentalItemType.Background);
        }

        private void LoadDataForSummary()
        {
            OrderPreview = _orderFacade.PrepareOrder(GetSelectedRentalItems(), GetSelectedProducts(),OrderBasicInfo);
        }

        public OrderSummaryModel OrderPreview { get; set; }

        private List<ProductModel> GetSelectedProducts()
        {
            return Products.Where(t=>SelectedProductIds.Contains(t.Id)).ToList();
        }

        private ICollection<RentalItemModel> GetSelectedRentalItems()
        {
            return new List<RentalItemModel>()
            {
                SelectedBackground,
                SelectedBooth,
            }.Union(SelectedProps).ToList();
        }

        private void LoadDataOrderMetadataFrom()
        {
            OrderBasicInfo=new OrderMatadata();
        }


        private void LoadDataForServiceSelect()
        {

            Packages = _catalogFacade.GetAllPackages();
            SelectedPackage = Packages.FirstOrDefault();
            Products = _catalogFacade.GetAvailableProducts();
            //SelectedRentalItemTypes.Add(RentalItemType.Background);
            //SelectedProductIds.Add(Products.FirstOrDefault().Id);
        }

        public void DeselectPackage()
        {
            SelectedPackage = null;
        }

        public void ClearProductSelection()
        {
            SelectedPackage = null;
            SelectedProductIds.Clear();
            SelectedRentalItemTypes.Clear();
        }

        private void LoadDataForBoothSelect()
        {
            Booths=_catalogFacade.GetAvailableRentalItems(OrderBasicInfo.Since,OrderBasicInfo.Till,RentalItemType.PhotoBooth);
        }

        public void HideAllSections()
        {
            OrderMetadataForm = false;
            ServiceSelect = false;
            BoothSelect = false;
            BackgroundSelect = false;
            PropsSelect = false;
            Summary = false;
        }

        public void GoToServicesSelection()
        {
            HideAllSections();
            ServiceSelect = true;
            LoadDataForServiceSelect();
        }

        public void UpdateItemsBasedOnSelectedPackage()
        {
            if (SelectedPackage==null)
                return;

            SelectedRentalItemTypes = SelectedPackage.RentalItemTypes;
            SelectedProductIds = SelectedPackage.ProductIds;
        }

        public void SelectPackageBasedOnItemSelection()
        {
            SelectedPackage = Packages.FirstOrDefault(t =>
                t.RentalItemTypes.Count == SelectedRentalItemTypes.Count && t.RentalItemTypes.All(s => SelectedRentalItemTypes.Contains(s)) &&
                t.ProductIds.Count == SelectedProductIds.Count && t.ProductIds.All(s => SelectedProductIds.Contains(s)));
        }

        public void ProductsSelectionChanged()
        {
            SelectPackageBasedOnItemSelection();
        }

        public void RentalItemSelectionChanged()
        {
            SelectPackageBasedOnItemSelection();
        }

        public void GoToProductsDetailSelection()
        {
            ValidateProductSelection();
            GoToBoothSelection();
        }

        public void GoToBoothSelection(bool back=false)
        {
            if (!SelectedRentalItemTypes.Contains(RentalItemType.PhotoBooth))
            {
                if (back)
                {
                    GoToServicesSelection();
                }
                else
                {
                    GoToBackgroundSelection();
                }
                return;
            }

            HideAllSections();
            BoothSelect = true;
        }

        private void ValidateProductSelection()
        {
            //TODO
        }

        public void SelectBooth(RentalItemModel booth)
        {
            if (booth.Type!=RentalItemType.PhotoBooth)
                throw new ArgumentException("Provided rental item is not booth!");

            SelectedBooth = booth;
            GoToBackgroundSelection();
        }
        public void SelectBackground(RentalItemModel background)
        {
            if (background.Type!=RentalItemType.Background)
                throw new ArgumentException("Provided rental item is not background!");

            SelectedBackground = background;
            GoToPropsSelection();
        }

        public void GoToBackgroundSelection(bool back=false)
        {
            if (!SelectedRentalItemTypes.Contains(RentalItemType.Background))
            {
                if (back)
                {
                    GoToBoothSelection(true);
                }
                else
                {
                    GoToPropsSelection();
                }
                return;
            }
            HideAllSections();
            BackgroundSelect = true;
        }

        public void GoToPropsSelection(bool back = false)
        {
            if (!SelectedRentalItemTypes.Contains(RentalItemType.Prop))
            {
                if (back)
                {
                    GoToBackgroundSelection(true);
                }
                else
                {
                    GoToSummary();
                }
                return;
            }
            HideAllSections();
            PropsSelect = true;
        }

        public void GoToSummary()
        {
            HideAllSections();
            Summary= true;
        }

        public ICollection<RentalItemModel> Booths { get; set; }
        public RentalItemModel SelectedBooth { get; set; }


        public ICollection<RentalItemModel> Backgrounds { get; set; }
        public RentalItemModel SelectedBackground { get; set; }

        public ICollection<RentalItemModel> Props { get; set; }
        public ICollection<RentalItemModel> SelectedProps { get; set; } = new List<RentalItemModel>();


        public void SendOrder()
        {
            var id = _orderFacade.SubmitOrder(GetSelectedRentalItems(), GetSelectedProducts(), OrderBasicInfo);
            Context.RedirectToRoute($"OrderDetail/{id}");
        }
    }
}
