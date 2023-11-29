using JSandwiches.API.IRespository;
using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.Users;
using JSandwiches.Models.Data;

namespace JSandwiches.API.Respository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        #region Food Related 
        private IGenericRespository<MenuItem> _menuItem;
        private IGenericRespository<AddOn> _addOn;
        private IGenericRespository<MenuItemAddOn> _menuItemAddOn;
        private IGenericRespository<ItemCategory> _itemCategory;
        private IGenericRespository<ItemSubCategory> _itemSubCategory;
        #endregion

        #region Order related
        private IGenericRespository<Order> _order;
        private IGenericRespository<OrderStatus> _orderStatus;
        private IGenericRespository<Receipt> _receipt;
        #endregion

        #region Special Features related
        private IGenericRespository<LoyaltyPoint> _loyaltyPoint;
        private IGenericRespository<CustomerLoyaltyPoint> _customerLoyaltyPoint;
        private IGenericRespository<Deal> _deal;
        private IGenericRespository<DealSpecifics> _dealSpecifics;
        private IGenericRespository<Rating> _rating;
        private IGenericRespository<MenuItemRating> _menuItemRating;
        #endregion

        #region Users related
        private IGenericRespository<Customer> _customer;
        private IGenericRespository<Address> _addresses;
        private IGenericRespository<CustomerAddress> _customerAddress;
        private IGenericRespository<Parish> _parish;
        #endregion


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Food Related 
        public IGenericRespository<MenuItem> MenuItem => _menuItem ??= new GenericRespository<MenuItem>(_context);
        public IGenericRespository<AddOn> AddOn => _addOn ??= new GenericRespository<AddOn>(_context);
        public IGenericRespository<MenuItemAddOn> MenuItemAddOn => _menuItemAddOn ??= new GenericRespository<MenuItemAddOn>(_context);
        public IGenericRespository<ItemCategory> ItemCategory => _itemCategory ??= new GenericRespository<ItemCategory>(_context);
        public IGenericRespository<ItemSubCategory> ItemSubCategory => _itemSubCategory ??= new GenericRespository<ItemSubCategory>(_context);
        #endregion

        #region Order related
        public IGenericRespository<Order> Order => _order ??= new GenericRespository<Order>(_context);
        public IGenericRespository<OrderStatus> OrderStatus => _orderStatus ??= new GenericRespository<OrderStatus>(_context);
        public IGenericRespository<Receipt> Receipt => _receipt ??= new GenericRespository<Receipt>(_context);
        #endregion

        #region Special Features related
        public IGenericRespository<LoyaltyPoint> LoyaltyPoint => _loyaltyPoint ??= new GenericRespository<LoyaltyPoint>(_context);
        public IGenericRespository<CustomerLoyaltyPoint> CustomerLoyaltyPoint => _customerLoyaltyPoint ??= new GenericRespository<CustomerLoyaltyPoint>(_context);
        public IGenericRespository<Deal> Deal => _deal ??= new GenericRespository<Deal>(_context);
        public IGenericRespository<DealSpecifics> DealSpecifics => _dealSpecifics ??= new GenericRespository<DealSpecifics>(_context);
        public IGenericRespository<Rating> Rating => _rating ??= new GenericRespository<Rating>(_context);
        public IGenericRespository<MenuItemRating> MenuItemRating => _menuItemRating ??= new GenericRespository<MenuItemRating>(_context);
        #endregion

        #region Users related
        public IGenericRespository<Customer> Customer => _customer ??= new GenericRespository<Customer>(_context);
        public IGenericRespository<Address> Address => _addresses ??= new GenericRespository<Address>(_context);
        public IGenericRespository<CustomerAddress> CustomerAddress => _customerAddress ??= new GenericRespository<CustomerAddress>(_context);
        public IGenericRespository<Parish> Parish => _parish ??= new GenericRespository<Parish>(_context);
        #endregion


        public void Dispose()
        {
            //free up memory that was being used
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
