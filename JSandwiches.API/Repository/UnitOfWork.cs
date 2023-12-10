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
        private IGenericRepository<MenuItem> _menuItem;
        private IGenericRepository<AddOn> _addOn;
        private IGenericRepository<MenuItemAddOn> _menuItemAddOn;
        private IGenericRepository<ItemCategory> _itemCategory;
        private IGenericRepository<ItemSubCategory> _itemSubCategory;
        #endregion

        #region Order related
        private IGenericRepository<Order> _order;
        private IGenericRepository<OrderStatus> _orderStatus;
        private IGenericRepository<Receipt> _receipt;
        private IGenericRepository<Payment> _payment;
        #endregion

        #region Special Features related
        private IGenericRepository<LoyaltyPoint> _loyaltyPoint;
        private IGenericRepository<CustomerLoyaltyPoint> _customerLoyaltyPoint;
        private IGenericRepository<Deal> _deal;
        private IGenericRepository<DealSpecifics> _dealSpecifics;
        private IGenericRepository<Rating> _rating;
        private IGenericRepository<MenuItemRating> _menuItemRating;
        #endregion

        #region Users related
        private IGenericRepository<Customer> _customer;
        private IGenericRepository<Address> _addresses;
        private IGenericRepository<CustomerAddress> _customerAddress;
        private IGenericRepository<Parish> _parish;
        #endregion


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Food Related 
        public IGenericRepository<MenuItem> MenuItem => _menuItem ??= new GenericRespository<MenuItem>(_context);
        public IGenericRepository<AddOn> AddOn => _addOn ??= new GenericRespository<AddOn>(_context);
        public IGenericRepository<MenuItemAddOn> MenuItemAddOn => _menuItemAddOn ??= new GenericRespository<MenuItemAddOn>(_context);
        public IGenericRepository<ItemCategory> ItemCategory => _itemCategory ??= new GenericRespository<ItemCategory>(_context);
        public IGenericRepository<ItemSubCategory> ItemSubCategory => _itemSubCategory ??= new GenericRespository<ItemSubCategory>(_context);
        #endregion

        #region Order related
        public IGenericRepository<Order> Order => _order ??= new GenericRespository<Order>(_context);
        public IGenericRepository<OrderStatus> OrderStatus => _orderStatus ??= new GenericRespository<OrderStatus>(_context);
        public IGenericRepository<Receipt> Receipt => _receipt ??= new GenericRespository<Receipt>(_context);
        public IGenericRepository<Payment> Payment => _payment ??= new GenericRespository<Payment>(_context);
        #endregion

        #region Special Features related
        public IGenericRepository<LoyaltyPoint> LoyaltyPoint => _loyaltyPoint ??= new GenericRespository<LoyaltyPoint>(_context);
        public IGenericRepository<CustomerLoyaltyPoint> CustomerLoyaltyPoint => _customerLoyaltyPoint ??= new GenericRespository<CustomerLoyaltyPoint>(_context);
        public IGenericRepository<Deal> Deal => _deal ??= new GenericRespository<Deal>(_context);
        public IGenericRepository<DealSpecifics> DealSpecifics => _dealSpecifics ??= new GenericRespository<DealSpecifics>(_context);
        public IGenericRepository<Rating> Rating => _rating ??= new GenericRespository<Rating>(_context);
        public IGenericRepository<MenuItemRating> MenuItemRating => _menuItemRating ??= new GenericRespository<MenuItemRating>(_context);
        #endregion

        #region Users related
        public IGenericRepository<Customer> Customer => _customer ??= new GenericRespository<Customer>(_context);
        public IGenericRepository<Address> Address => _addresses ??= new GenericRespository<Address>(_context);
        public IGenericRepository<CustomerAddress> CustomerAddress => _customerAddress ??= new GenericRespository<CustomerAddress>(_context);
        public IGenericRepository<Parish> Parish => _parish ??= new GenericRespository<Parish>(_context);
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
