using AutoMapper;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.Models.Users;
using JSandwiches.Models.DTO.UsersDTO;

namespace JSandwiches.MVC
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            #region Food Related 
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
            CreateMap<MenuItem, CreateMenuItemDTO>().ReverseMap();
            CreateMap<AddOn, AddOnDTO>().ReverseMap();
            CreateMap<AddOn, CreateAddOnDTO>().ReverseMap();
            CreateMap<MenuItemAddOn, MenuItemAddOnDTO>().ReverseMap();
            CreateMap<MenuItemAddOn, CreateMenuItemAddOnDTO>().ReverseMap();
            CreateMap<ItemCategory, ItemCategoryDTO>().ReverseMap();
            CreateMap<ItemCategory, CreateItemCategoryDTO>().ReverseMap();
            CreateMap<ItemSubCategory, ItemSubCategoryDTO>().ReverseMap();
            CreateMap<ItemSubCategory, CreateItemSubCategoryDTO>().ReverseMap();
            #endregion

            #region Order related
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusDTO>().ReverseMap();
            CreateMap<Receipt, ReceiptDTO>().ReverseMap();
            CreateMap<Receipt, CreateReceiptDTO>().ReverseMap();
            #endregion

            #region Special Features related
            CreateMap<LoyaltyPoint, LoyaltyPointDTO>().ReverseMap();
            CreateMap<LoyaltyPoint, CreateLoyaltyPointDTO>().ReverseMap();
            CreateMap<CustomerLoyaltyPoint, CustomerLoyaltyPointDTO>().ReverseMap();
            CreateMap<CustomerLoyaltyPoint, CreateCustomerLoyaltyPointDTO>().ReverseMap();
            CreateMap<Deal, DealDTO>().ReverseMap();
            CreateMap<Deal, CreateDealDTO>().ReverseMap();
            CreateMap<DealSpecifics, DealSpecificsDTO>().ReverseMap();
            CreateMap<DealSpecifics, CreateDealSpecificsDTO>().ReverseMap();
            CreateMap<Rating, RatingDTO>().ReverseMap();
            CreateMap<MenuItemRating, MenuItemRatingDTO>().ReverseMap();
            CreateMap<MenuItemRating, CreateMenuItemRatingDTO>().ReverseMap();
            #endregion

            #region Users related
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Address, CreateAddressDTO>().ReverseMap();
            CreateMap<CustomerAddress, CustomerAddressDTO>().ReverseMap();
            CreateMap<CustomerAddress, CreateCustomerAddressDTO>().ReverseMap();
            CreateMap<Parish, ParishDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, LoginAppUserDTO>().ReverseMap();
            #endregion
        }

    }
}

