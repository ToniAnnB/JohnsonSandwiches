using AutoMapper;
using JSandwiches.Models.DTO.FoodDTO;
using JSandwiches.Models.DTO.OrderDTO;
using JSandwiches.Models.DTO.SpecialFeaturesDTO;
using JSandwiches.Models.DTO.UsersDTO;
using JSandwiches.MVC.IRepository;

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
        private IGenConsumRepo<MenuItemDTO> _menuItem;
        private IGenConsumRepo<AddOnDTO> _addOn;
        private IGenConsumRepo<MenuItemAddOnDTO> _menuItemAddOn;
        private IGenConsumRepo<ItemCategoryDTO> _itemCategory;
        private IGenConsumRepo<ItemSubCategoryDTO> _itemSubCategory;
        #endregion

        #region Order related
        private IGenConsumRepo<OrderDTO> _order;
        private IGenConsumRepo<OrderStatusDTO> _orderStatus;
        private IGenConsumRepo<ReceiptDTO> _receipt;
        private IGenConsumRepo<PaymentDTO> _payment;
        #endregion

        #region Special Features related
        private IGenConsumRepo<LoyaltyPointDTO> _loyaltyPoint;
        private IGenConsumRepo<CustomerLoyaltyPointDTO> _customerLoyaltyPoint;
        private IGenConsumRepo<DealDTO> _deal;
        private IGenConsumRepo<DealSpecificsDTO> _dealSpecifics;
        private IGenConsumRepo<RatingDTO> _rating;
        private IGenConsumRepo<MenuItemRatingDTO> _menuItemRating;
        #endregion

        #region Users related
        private IGenConsumRepo<CustomerDTO> _customer;
        private IGenConsumRepo<AddressDTO> _address;
        private IGenConsumRepo<CustomerAddressDTO> _customerAddress;
        private IGenConsumRepo<ParishDTO> _parish;
        #endregion

        #region Food Related 
        public IGenConsumRepo<MenuItemDTO> MenuItem => _menuItem ??= new GenConsumRepo<MenuItemDTO>("https://localhost:44381/api/MenuItem", _mapper);
        public IGenConsumRepo<AddOnDTO> AddOn => _addOn ??= new GenConsumRepo<AddOnDTO>("https://localhost:44381/api/AddOn", _mapper);
        public IGenConsumRepo<MenuItemAddOnDTO> MenuItemAddOn => _menuItemAddOn ??= new GenConsumRepo<MenuItemAddOnDTO>("https://localhost:44381/api/MenuItemAddOn", _mapper);
        public IGenConsumRepo<ItemCategoryDTO> ItemCategory => _itemCategory ??= new GenConsumRepo<ItemCategoryDTO>("https://localhost:44381/api/ItemCategory", _mapper);
        public IGenConsumRepo<ItemSubCategoryDTO> ItemSubCategory => _itemSubCategory ??= new GenConsumRepo<ItemSubCategoryDTO>("https://localhost:44381/api/ItemSubCategory", _mapper);
        #endregion

        #region Order related
        public IGenConsumRepo<OrderDTO> Order => _order ??= new GenConsumRepo<OrderDTO>("https://localhost:44381/api/Order", _mapper);
        public IGenConsumRepo<OrderStatusDTO> OrderStatus => _orderStatus ??= new GenConsumRepo<OrderStatusDTO>("https://localhost:44381/api/OrderStatus", _mapper);
        public IGenConsumRepo<ReceiptDTO> Receipt => _receipt ??= new GenConsumRepo<ReceiptDTO>("https://localhost:44381/api/Receipt", _mapper);
        public IGenConsumRepo<PaymentDTO> Payment => _payment ??= new GenConsumRepo<PaymentDTO>("https://localhost:44381/api/Payment", _mapper);
        #endregion

        #region Special Features related
        public IGenConsumRepo<LoyaltyPointDTO> LoyaltyPoint => _loyaltyPoint ??= new GenConsumRepo<LoyaltyPointDTO>("https://localhost:44381/api/LoyaltyPoint", _mapper);
        public IGenConsumRepo<CustomerLoyaltyPointDTO> CustomerLoyaltyPoint => _customerLoyaltyPoint ??= new GenConsumRepo<CustomerLoyaltyPointDTO>("https://localhost:44381/api/CustomerLoyaltyPoint", _mapper);
        public IGenConsumRepo<DealDTO> Deal => _deal ??= new GenConsumRepo<DealDTO>("https://localhost:44381/api/Deal", _mapper);
        public IGenConsumRepo<DealSpecificsDTO> DealSpecifics => _dealSpecifics ??= new GenConsumRepo<DealSpecificsDTO>("https://localhost:44381/api/DealSpecifics", _mapper);
        public IGenConsumRepo<RatingDTO> Rating => _rating ??= new GenConsumRepo<RatingDTO>("https://localhost:44381/api/Rating", _mapper);
        public IGenConsumRepo<MenuItemRatingDTO> MenuItemRating => _menuItemRating ??= new GenConsumRepo<MenuItemRatingDTO>("https://localhost:44381/api/MenuItemRating", _mapper);
        #endregion

        #region Users related
        public IGenConsumRepo<CustomerDTO> Customer => _customer ??= new GenConsumRepo<CustomerDTO>("https://localhost:44381/api/Customer", _mapper);
        public IGenConsumRepo<AddressDTO> Address => _address ??= new GenConsumRepo<AddressDTO>("https://localhost:44381/api/Address", _mapper);
        public IGenConsumRepo<CustomerAddressDTO> CustomerAddress => _customerAddress ??= new GenConsumRepo<CustomerAddressDTO>("https://localhost:44381/api/CustomerAddress", _mapper);
        public IGenConsumRepo<ParishDTO> Parish => _parish ??= new GenConsumRepo<ParishDTO>("https://localhost:44381/api/Parish", _mapper);
        #endregion

    }
}
