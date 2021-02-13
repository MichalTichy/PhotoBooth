using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.AspNetCore.Identity;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.BL.Models.User;
using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoBooth.WEB.ViewModels
{
    public class OrderProcessViewModel : MasterPageViewModel
    {
        private readonly ICatalogFacade _catalogFacade;
        private readonly IOrderFacade _orderFacade;
        private readonly UserFacade _userFacade;

        public OrderProcessViewModel(ICatalogFacade catalogFacade, IOrderFacade orderFacade, UserFacade userFacade)
        {
            _catalogFacade = catalogFacade;
            _orderFacade = orderFacade;
            _userFacade = userFacade;
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
        public ICollection<RentalItemType> AvailableRentalTypes { get; set; } = new List<RentalItemType>();
        public bool PropsAvailable => AvailableRentalTypes.Contains(RentalItemType.Prop);
        public bool BackgroundAvailable => AvailableRentalTypes.Contains(RentalItemType.Background);
        public bool PhotoBoothAvailable => AvailableRentalTypes.Contains(RentalItemType.PhotoBooth);
        public ICollection<RentalItemModel> AvailableRentalItems { get; set; }

        public ItemPackageDTO SelectedPackage { get; set; }

        public OrderSummaryModel OrderPreview { get; set; } = new OrderSummaryModel();

        private List<ProductModel> GetSelectedProducts()
        {
            return Products.Where(t => SelectedProductIds.Contains(t.Id)).ToList();
        }

        public async Task CreateUser()
        {
            var userInfo = await _userFacade.RegisterSendTemporaryPasswordAsync(OrderBasicInfo.User);
            if (userInfo.Succeeded)
            {
                var user = await _userFacade.GetIdentityByUsername(OrderBasicInfo.User.Email);
                await Context.GetAuthentication().SignInAsync(IdentityConstants.ApplicationScheme, new ClaimsPrincipal(user));
                //TODO display result
                HideAllSections();

                PrepareSummary();

                Summary = true;
            }
            else
            {
                this.Context.ModelState.Errors.Add(new DotVVM.Framework.ViewModel.Validation.ViewModelValidationError() { PropertyPath = nameof(OrderBasicInfo.User.Email), ErrorMessage = "Ucet so zadanym e-mailom uz existuje!" });
                Context.FailOnInvalidModelState();
            }
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
            //calling DB
            AvailableRentalItems = _catalogFacade.GetAvailableRentalItemsAsync(OrderBasicInfo.Since, OrderBasicInfo.Since.AddHours(OrderBasicInfo.CountOfHours)).Result;
            Packages = _catalogFacade.GetAllPackagesAsync().Result;
            Products = _catalogFacade.GetAvailableProductsAsync().Result;

            AvailableRentalTypes = AvailableRentalItems.Select(a => a.Type).Distinct().Where(a => a != RentalItemType.Employe).ToList();

            //when we do "default selection" it does not bind correctly
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

            if (SelectedRentalItemTypes.Contains(RentalItemType.PhotoBooth))
            {
                BoothSelect = true;
            }
            if (SelectedRentalItemTypes.Contains(RentalItemType.Background))
            {
                BackgroundSelect = true;
            }
            if (SelectedRentalItemTypes.Contains(RentalItemType.Prop))
            {
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
            if (SelectedPackage == null)
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

        public async Task GoToSummary()
        {
            HideAllSections();
            var username = Context?.HttpContext?.User?.Identity?.Name;
            if (username == null)
            {
                UserInfoSelect = true;
                OrderBasicInfo.User = new ApplicationUserListModel();
            }
            else
            {
                var applicationUser = await _userFacade.GetUserByUsername(username);
                OrderBasicInfo.User = new ApplicationUserListModel()
                {
                    Id = new Guid(applicationUser.Id),
                    Email = applicationUser.Email,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    PhoneNumber = applicationUser.PhoneNumber
                };
                PrepareSummary();

                Summary = true;
            }
        }

        private void PrepareSummary()
        {
            var rentalItems = new List<RentalItemModel>();
            rentalItems.AddRange(SelectedProps);

            if (SelectedBooth != null) rentalItems.Add(SelectedBooth);
            if (SelectedBackground != null) rentalItems.Add(SelectedBackground);

            OrderPreview = _orderFacade.PrepareOrderAsync(rentalItems, Products.Where(t => SelectedProductIds.Contains(t.Id)).ToList(), OrderBasicInfo).Result;
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
            }.Union(SelectedProps).Where(a => a != null).ToList();
        }

        public async Task SendOrder()
        {
            var order = await _orderFacade.SubmitOrderAsync(GetSelectedRentalItems(), GetSelectedProducts(), OrderBasicInfo);

            Context.RedirectToRoute($"OrderDetail", new { id = order.Id });
        }
    }
}