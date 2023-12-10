using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.Users;

namespace JSandwiches.API.IRespository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<AddOn> AddOn { get; }
        IGenericRepository<Address> Address { get; }
        IGenericRepository<Customer> Customer { get; }
        IGenericRepository<CustomerAddress> CustomerAddress { get; }
        IGenericRepository<CustomerLoyaltyPoint> CustomerLoyaltyPoint { get; }
        IGenericRepository<Deal> Deal { get; }
        IGenericRepository<DealSpecifics> DealSpecifics { get; }
        IGenericRepository<ItemCategory> ItemCategory { get; }
        IGenericRepository<ItemSubCategory> ItemSubCategory { get; }
        IGenericRepository<LoyaltyPoint> LoyaltyPoint { get; }
        IGenericRepository<MenuItem> MenuItem { get; }
        IGenericRepository<MenuItemAddOn> MenuItemAddOn { get; }
        IGenericRepository<MenuItemRating> MenuItemRating { get; }
        IGenericRepository<Order> Order { get; }
        IGenericRepository<Payment> Payment { get; }
        IGenericRepository<OrderStatus> OrderStatus { get; }
        IGenericRepository<Parish> Parish { get; }
        IGenericRepository<Rating> Rating { get; }
        IGenericRepository<Receipt> Receipt { get; }

        void Dispose();
        Task Save();

    }
}
