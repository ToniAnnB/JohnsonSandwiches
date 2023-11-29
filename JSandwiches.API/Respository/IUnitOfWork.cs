using JSandwiches.API.IRespository;
using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.Users;

namespace ApplicationAPI.Respository
{
    public interface IUnitOfWork
    {
        IGenericRespository<AddOn> AddOn { get; }
        IGenericRespository<Address> Address { get; }
        IGenericRespository<Customer> Customer { get; }
        IGenericRespository<CustomerAddress> CustomerAddress { get; }
        IGenericRespository<CustomerLoyaltyPoint> CustomerLoyalPoint { get; }
        IGenericRespository<Deal> Deal { get; }
        IGenericRespository<DealSpecifics> DealSpecifics { get; }
        IGenericRespository<ItemCategory> ItemCategory { get; }
        IGenericRespository<ItemSubCategory> ItemSubCategory { get; }
        IGenericRespository<LoyaltyPoint> LoyalPoint { get; }
        IGenericRespository<MenuItem> MenuItem { get; }
        IGenericRespository<MenuItemAddOn> MenuItemAddOn { get; }
        IGenericRespository<MenuItemRating> MenuItemRating { get; }
        IGenericRespository<Order> Order { get; }
        IGenericRespository<OrderStatus> OrderStatus { get; }
        IGenericRespository<Parish> Parish { get; }
        IGenericRespository<Rating> Rating { get; }
        IGenericRespository<Reciept> Reciept { get; }

        void Dispose();
        Task Save();
    }
}