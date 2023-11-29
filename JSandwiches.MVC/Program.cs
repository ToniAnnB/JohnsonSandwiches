using JSandwiches.MVC;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Respository;
using Microsoft.AspNetCore.Mvc.ViewComponents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region Transient Services
// registering the ConsumUnitOfWork class with the dependency injection (registers the service with a transient lifetime)
builder.Services.AddTransient<IConsumUnitOfWork, ConsumUnitOfWork>();
#endregion

builder.Services.AddAutoMapper(typeof(MappingConfig));

//builder.Services.AddScoped<IViewComponentInvoker>();
builder.Services.AddRazorPages();
//builder.Services.AddRazorComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
//app.MapComponents();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
