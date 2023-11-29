using JSandwiches.Models.Food;
using JSandwiches.Models.Order;
using JSandwiches.Models.SpecialFeatures;
using JSandwiches.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JSandwiches.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }


        #region Food Related 
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<AddOn> AddOn { get; set; }
        public DbSet<MenuItemAddOn> MenuItemAddOn { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ItemSubCategory> ItemSubCategory { get; set; }
        #endregion

        #region Order related
        public DbSet<Order.Order> Order { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        #endregion

        #region Special Features related
        public DbSet<LoyaltyPoint> LoyaltyPoint { get; set; }
        public DbSet<CustomerLoyaltyPoint> CustomerLoyaltyPoint { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<DealSpecifics> DealSpecifics { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<MenuItemRating> MenuItemRating { get; set; }
        #endregion

        #region Users related
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Parish> Parish { get; set; }
        #endregion

    }
}
