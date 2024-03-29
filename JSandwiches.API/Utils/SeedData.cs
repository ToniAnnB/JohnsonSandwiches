﻿using Microsoft.AspNetCore.Identity;

namespace JSandwiches.Utils
{
    public static class SeedData
    {
        public static async Task Intialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await SeedRoles(roleManager);
            await SeedAdminUser(userManager);
            await SeedCustomerUser(userManager);
            await SeedKitchenUser(userManager);
            await SeedCServiceUser(userManager);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Customer", "Kitchen", "CService" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminUser(UserManager<IdentityUser> userManager)
        {
            var adminUser = await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                var admin = new IdentityUser()
                {
                    UserName = "admin",
                    Email = "admin@email.com",

                };


                var createAdmin = await userManager.CreateAsync(admin, "Admin123$");

                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
        public static async Task SeedCustomerUser(UserManager<IdentityUser> userManager)
        {
            var customerUser = await userManager.FindByNameAsync("customer");

            if (customerUser == null)
            {
                var customer = new IdentityUser()
                {
                    UserName = "customer",
                    Email = "customer@email.com",

                };


                var createCustomer = await userManager.CreateAsync(customer, "Customer123$");

                if (createCustomer.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "Customer");
                }
            }
        }
        public static async Task SeedKitchenUser(UserManager<IdentityUser> userManager)
        {
            var kitchenUser = await userManager.FindByNameAsync("kitchenStaff");

            if (kitchenUser == null)
            {
                var kitchenStaff = new IdentityUser()
                {
                    UserName = "kitchenStaff",
                    Email = "kStaff@email.com",
                };


                var createCustomer = await userManager.CreateAsync(kitchenStaff, "Kitchen123$");

                if (createCustomer.Succeeded)
                {
                    await userManager.AddToRoleAsync(kitchenStaff, "Kitchen");
                }
            }
        }
        public static async Task SeedCServiceUser(UserManager<IdentityUser> userManager)
        {
            var customerServiceUser = await userManager.FindByNameAsync("customerService");

            if (customerServiceUser == null)
            {
                var customerService = new IdentityUser()
                {
                    UserName = "customerService",
                    Email = "customerService@email.com",

                };


                var createCService = await userManager.CreateAsync(customerService, "CService123$");

                if (createCService.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerService, "CService");
                }
            }
        }

    }
}
