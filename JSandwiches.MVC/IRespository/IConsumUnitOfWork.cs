using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.Models.DTO.UsersDTO;

namespace JSandwiches.MVC.IRespository
{
    public interface IConsumUnitOfWork
    {
        #region Food Related 
         IGenConsumRespo<MenuItemDTO> MenuItem {get;}
         IGenConsumRespo<AddOnDTO> AddOn {get;}
         IGenConsumRespo<MenuItemAddOnDTO> MenuItemAddOn {get;}
         IGenConsumRespo<ItemCategoryDTO> ItemCategory {get;}
         IGenConsumRespo<ItemSubCategoryDTO> ItemSubCategory {get;}
        #endregion

        #region Order related
         IGenConsumRespo<OrderDTO> Order {get;}
         IGenConsumRespo<OrderStatusDTO> OrderStatus {get;}
         IGenConsumRespo<ReceiptDTO> Receipt {get;}
        #endregion

        #region Special Features related
         IGenConsumRespo<LoyaltyPointDTO> LoyaltyPoint {get;}
         IGenConsumRespo<CustomerLoyaltyPointDTO> CustomerLoyaltyPoint {get;}
         IGenConsumRespo<DealDTO> Deal {get;}
         IGenConsumRespo<DealSpecificsDTO> DealSpecifics {get;}
         IGenConsumRespo<RatingDTO> Rating {get;}
         IGenConsumRespo<MenuItemRatingDTO> MenuItemRating {get;}
        #endregion

        #region Users related
         IGenConsumRespo<CustomerDTO> Customer {get;}
         IGenConsumRespo<AddressDTO> Address {get;}
         IGenConsumRespo<CustomerAddressDTO> CustomerAddress {get;}
         IGenConsumRespo<ParishDTO> Parish {get;}
        #endregion

    }
}
