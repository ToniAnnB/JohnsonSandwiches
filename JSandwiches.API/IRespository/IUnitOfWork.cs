using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.Users;

namespace JSandwiches.API.IRespository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRespository<AddOn> AddOn { get; }
        IGenericRespository<Address> Address { get; }
        IGenericRespository<Customer> Customer { get; }
        IGenericRespository<CustomerAddress> CustomerAddress { get; }
        IGenericRespository<CustomerLoyaltyPoint> CustomerLoyaltyPoint { get; }
        IGenericRespository<Deal> Deal { get; }
        IGenericRespository<DealSpecifics> DealSpecifics { get; }
        IGenericRespository<ItemCategory> ItemCategory { get; }
        IGenericRespository<ItemSubCategory> ItemSubCategory { get; }
        IGenericRespository<LoyaltyPoint> LoyaltyPoint { get; }
        IGenericRespository<MenuItem> MenuItem { get; }
        IGenericRespository<MenuItemAddOn> MenuItemAddOn { get; }
        IGenericRespository<MenuItemRating> MenuItemRating { get; }
        IGenericRespository<Order> Order { get; }
        IGenericRespository<OrderStatus> OrderStatus { get; }
        IGenericRespository<Parish> Parish { get; }
        IGenericRespository<Rating> Rating { get; }
        IGenericRespository<Receipt> Receipt { get; }

        void Dispose();
        Task Save();

    }
}
