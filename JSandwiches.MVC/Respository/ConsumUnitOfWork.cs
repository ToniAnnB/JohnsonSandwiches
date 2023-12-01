using AutoMapper;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.Food;
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
        private IGenConsumRespo<AddressDTO> _address;
        private IGenConsumRespo<CustomerAddressDTO> _customerAddress;
        private IGenConsumRespo<ParishDTO> _parish;
        #endregion

        #region Food Related 
        public IGenConsumRespo<MenuItemDTO> MenuItem => _menuItem??= new GenConsumRespo<MenuItemDTO>("https://localhost:44381/api/MenuItem", _mapper);
        public IGenConsumRespo<AddOnDTO> AddOn => _addOn??= new GenConsumRespo<AddOnDTO>("https://localhost:44381/api/AddOn", _mapper);
        public IGenConsumRespo<MenuItemAddOnDTO> MenuItemAddOn => _menuItemAddOn??= new GenConsumRespo<MenuItemAddOnDTO>("https://localhost:44381/api/MenuItemAddOn", _mapper);
        public IGenConsumRespo<ItemCategoryDTO> ItemCategory => _itemCategory??= new GenConsumRespo<ItemCategoryDTO>("https://localhost:44381/api/ItemCategory", _mapper);
        public IGenConsumRespo<ItemSubCategoryDTO> ItemSubCategory => _itemSubCategory??= new GenConsumRespo<ItemSubCategoryDTO>("https://localhost:44381/api/ItemSubCategory", _mapper);
        #endregion

        #region Order related
        public IGenConsumRespo<OrderDTO> Order => _order??= new GenConsumRespo<OrderDTO>("https://localhost:44381/api/Order", _mapper);
        public IGenConsumRespo<OrderStatusDTO> OrderStatus => _orderStatus??= new GenConsumRespo<OrderStatusDTO>("https://localhost:44381/api/OrderStatus", _mapper);
        public IGenConsumRespo<ReceiptDTO> Receipt=> _receipt??= new GenConsumRespo<ReceiptDTO>("https://localhost:44381/api/Receipt", _mapper);
        #endregion

        #region Special Features related
        public IGenConsumRespo<LoyaltyPointDTO> LoyaltyPoint => _loyaltyPoint??= new GenConsumRespo<LoyaltyPointDTO>("https://localhost:44381/api/LoyaltyPoint", _mapper);
        public IGenConsumRespo<CustomerLoyaltyPointDTO> CustomerLoyaltyPoint =>  _customerLoyaltyPoint??= new GenConsumRespo<CustomerLoyaltyPointDTO>("https://localhost:44381/api/CustomerLoyaltyPoint", _mapper);
        public IGenConsumRespo<DealDTO> Deal => _deal??= new GenConsumRespo<DealDTO>("https://localhost:44381/api/Deal", _mapper);
        public IGenConsumRespo<DealSpecificsDTO> DealSpecifics => _dealSpecifics??= new GenConsumRespo<DealSpecificsDTO>("https://localhost:44381/api/DealSpecifics", _mapper);
        public IGenConsumRespo<RatingDTO> Rating => _rating??= new GenConsumRespo<RatingDTO>("https://localhost:44381/api/Rating", _mapper);
        public IGenConsumRespo<MenuItemRatingDTO> MenuItemRating => _menuItemRating??= new GenConsumRespo<MenuItemRatingDTO>("https://localhost:44381/api/MenuItemRating", _mapper);
        #endregion

        #region Users related
        public IGenConsumRespo<CustomerDTO> Customer => _customer??= new GenConsumRespo<CustomerDTO>("https://localhost:44381/api/Customer", _mapper);
        public IGenConsumRespo<AddressDTO> Address => _address??= new GenConsumRespo<AddressDTO>("https://localhost:44381/api/Address", _mapper);
        public IGenConsumRespo<CustomerAddressDTO> CustomerAddress => _customerAddress??= new GenConsumRespo<CustomerAddressDTO>("https://localhost:44381/api/CustomerAddress", _mapper);
        public IGenConsumRespo<ParishDTO> Parish => _parish??= new GenConsumRespo<ParishDTO>("https://localhost:44381/api/Parish", _mapper);
        #endregion

    }
}
