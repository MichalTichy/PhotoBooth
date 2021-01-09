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
using DotVVM.Framework.ViewModel.Validation;

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
        public bool DetailedServicesSelect { get; set; } = false;
        public bool UserInfoSelect { get; set; } = false;
        public bool Summary { get; set; } = false;

        [Required]
        public OrderMatadata OrderBasicInfo { get; set; } = new OrderMatadata();
        public ICollection<ItemPackageDTO> Packages { get; set; }
        public bool CustomPackage => SelectedPackage == null;
        public ICollection<ProductModel> Products { get; set; }
        public ICollection<RentalItemType> SelectedRentalItemTypes { get; set; } = new List<RentalItemType>();
        public ICollection<Guid> SelectedProductIds { get; set; } = new List<Guid>();
        public ICollection<RentalItemType> AvailableRentalTypes { get; set; }
        public ICollection<RentalItemModel> AvailableRentalItems { get; set; }
        public ICollection<RentalItemModel> SelectedRentalItems { get; set; }

        public ItemPackageDTO SelectedPackage { get; set; }

        //CREATING JUST FOR TESTING PURPOSES - ASSIGNMENT SHOULD BE DONE IN PrepareSummary()
        public OrderSummaryModel OrderPreview { get; set; } = new OrderSummaryModel();

        private List<ProductModel> GetSelectedProducts()
        {
            return Products.Where(t=>SelectedProductIds.Contains(t.Id)).ToList();
        }

        public void HideAllSections()
        {
            OrderMetadataForm = false;
            ServiceSelect = false;
            DetailedServicesSelect = false;
            BoothSelect = false;
            BackgroundSelect = false;
            PropsSelect = false;
            UserInfoSelect = false;
            Summary = false;
        }

        private void LoadDataForServiceSelect()
        {
            OrderBasicInfo.Till = OrderBasicInfo.Since.AddHours(OrderBasicInfo.CountOfHours);
            //calling DB
            AvailableRentalItems = _catalogFacade.GetAvailableRentalItems(OrderBasicInfo.Since, OrderBasicInfo.Till);
            Packages = _catalogFacade.GetAllPackages();
            Products = _catalogFacade.GetAvailableProducts();
            
            AvailableRentalTypes = AvailableRentalItems.Select(a => a.Type).Distinct().Where(a=>a!=RentalItemType.Employe).ToList();
            //SelectedPackage = Packages.FirstOrDefault();
            UpdateItemsBasedOnSelectedPackage();
        }

        //whenever selection is changed, selected package is changed
        public void DeselectPackage()
        {
            SelectedPackage = null;
        }

        //when custom package is selected
        public void ClearProductSelection()
        {
            SelectedPackage = null;
            SelectedProductIds.Clear();
            SelectedRentalItemTypes.Clear();
        }

        private void LoadDataForDeatiledServicesSelect()
        {
            if (Booths == null)
            {
                Booths = AvailableRentalItems.Where(a => a.Type == RentalItemType.PhotoBooth).ToList();
                //SelectedBooth = Booths.FirstOrDefault();
            }

            if (Backgrounds == null)
            {
                Backgrounds = AvailableRentalItems.Where(a => a.Type == RentalItemType.Background).ToList();
                //SelectedBackground = Backgrounds.FirstOrDefault();
            }

            if (Props == null)
            {
                Props = AvailableRentalItems.Where(a => a.Type == RentalItemType.Prop).ToList();
            }
            

            if (SelectedRentalItemTypes.Contains(RentalItemType.PhotoBooth)) {
                BoothSelect = true;
            }
            if (SelectedRentalItemTypes.Contains(RentalItemType.Background)) {
                BackgroundSelect = true;
            }
            if (SelectedRentalItemTypes.Contains(RentalItemType.Prop)) {
                PropsSelect = true;
            }
        }


        public void GoToServicesSelection(bool loadData = true)
        {
            if (loadData)
            {
                LoadDataForServiceSelect();
            }
            HideAllSections();
            ServiceSelect = true;
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

        public void GoToDetailServicesSelection()
        {
            ValidateProductSelection();
            HideAllSections();
            LoadDataForDeatiledServicesSelect();
            DetailedServicesSelect = true;
        }

        private void ValidateProductSelection()
        {
            //TODO
        }

        public void GoToSummary()
        {
            PrepareSummary();
            HideAllSections();
            Summary= true;
        }

        private void PrepareSummary()
        {
            OrderPreview = new OrderSummaryModel();
            //TODO
        }

        public ICollection<RentalItemModel> Booths { get; set; }
        public RentalItemModel SelectedBooth { get; set; }


        public ICollection<RentalItemModel> Backgrounds { get; set; }
        public RentalItemModel SelectedBackground { get; set; }

        public ICollection<RentalItemModel> Props { get; set; }
        public ICollection<RentalItemModel> SelectedProps { get; set; } = new List<RentalItemModel>();

        private ICollection<RentalItemModel> GetSelectedRentalItems()
        {
            return new List<RentalItemModel>()
            {
                SelectedBackground,
                SelectedBooth,
            }.Union(SelectedProps).ToList();
        }

        public void SendOrder()
        {
            var id = _orderFacade.SubmitOrder(GetSelectedRentalItems(), GetSelectedProducts(), OrderBasicInfo);
            Context.RedirectToRoute($"OrderDetail/{id}");
        }
    }
}
