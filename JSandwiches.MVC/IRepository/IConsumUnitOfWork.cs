using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.Models.DTO.UsersDTO;

namespace JSandwiches.MVC.IRepository
{
    public interface IConsumUnitOfWork
    {
        #region Food Related 
         IGenConsumRepo<MenuItemDTO> MenuItem {get;}
         IGenConsumRepo<AddOnDTO> AddOn {get;}
         IGenConsumRepo<MenuItemAddOnDTO> MenuItemAddOn {get;}
         IGenConsumRepo<ItemCategoryDTO> ItemCategory {get;}
         IGenConsumRepo<ItemSubCategoryDTO> ItemSubCategory {get;}
        #endregion

        #region Order related
         IGenConsumRepo<OrderDTO> Order {get;}
         IGenConsumRepo<OrderStatusDTO> OrderStatus {get;}
         IGenConsumRepo<ReceiptDTO> Receipt {get;}
         IGenConsumRepo<PaymentDTO> Payment {get;}
        #endregion

        #region Special Features related
         IGenConsumRepo<LoyaltyPointDTO> LoyaltyPoint {get;}
         IGenConsumRepo<CustomerLoyaltyPointDTO> CustomerLoyaltyPoint {get;}
         IGenConsumRepo<DealDTO> Deal {get;}
         IGenConsumRepo<DealSpecificsDTO> DealSpecifics {get;}
         IGenConsumRepo<RatingDTO> Rating {get;}
         IGenConsumRepo<MenuItemRatingDTO> MenuItemRating {get;}
        #endregion

        #region Users related
         IGenConsumRepo<CustomerDTO> Customer {get;}
         IGenConsumRepo<AddressDTO> Address {get;}
         IGenConsumRepo<CustomerAddressDTO> CustomerAddress {get;}
         IGenConsumRepo<ParishDTO> Parish {get;}
        #endregion

    }
}
