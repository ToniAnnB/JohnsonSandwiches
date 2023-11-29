using AutoMapper;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.OrderDTO;
using JSandwiches.Models.SpecialFeaturesDTO;
using JSandwiches.Models.UsersDTO;
using JSandwiches.MVC.IRespository;

namespace JSandwiches.MVC.Respository
{
    public class ConsumUnitOfWork : IConsumUnitOfWork
    {
        public readonly IMapper _mapper;
        public ConsumUnitOfWork(IMapper mapper)
        {
            _mapper = mapper;
        }
        #region Food Related 
        private IGenConsumRespo<MenuItemDTO> _menuItem;
        private IGenConsumRespo<AddOnDTO> _addOn;
        private IGenConsumRespo<MenuItemAddOnDTO> _menuItemAddOn;
        private IGenConsumRespo<ItemCategoryDTO> _itemCategory;
        private IGenConsumRespo<ItemSubCategoryDTO> _itemSubCategory;
        #endregion

        #region Order related
        private IGenConsumRespo<OrderDTO> _order;
        private IGenConsumRespo<OrderStatusDTO> _orderStatus;
        private IGenConsumRespo<ReceiptDTO> _receipt;
        #endregion

        #region Special Features related
        private IGenConsumRespo<LoyaltyPointDTO> _loyaltyPoint;
        private IGenConsumRespo<CustomerLoyaltyPointDTO> _customerLoyaltyPoint;
        private IGenConsumRespo<DealDTO> _deal;
        private IGenConsumRespo<DealSpecificsDTO> _dealSpecifics;
        private IGenConsumRespo<RatingDTO> _rating;
        private IGenConsumRespo<MenuItemRatingDTO> _menuItemRating;
        #endregion

        #region Users related
        private IGenConsumRespo<CustomerDTO> _customer;
        private IGenConsumRespo<AddressDTO> _addresses;
        private IGenConsumRespo<CustomerAddressDTO> _customerAddress;
        private IGenConsumRespo<ParishDTO> _parish;
        #endregion

        public IGenConsumRespo<AddressDTO> Address => _addresses ??= new GenConsumRespo<AddressDTO>("https://localhost:44381/api/Address", _mapper);
        public IGenConsumRespo<AddOnDTO> AddOn => _addOn ??= new GenConsumRespo<AddOnDTO>("https://localhost:44381/api/AddOn", _mapper);

    }
}
