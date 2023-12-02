using JSandwiches.MVC;
using JSandwiches.MVC.IRespository;
using JSandwiches.MVC.Respository;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region Transient Services
// registering the ConsumUnitOfWork class with the dependency injection (registers the service with a transient lifetime)
builder.Services.AddTransient<IConsumUnitOfWork, ConsumUnitOfWork>();
#endregion

builder.Services.AddHttpClient("AuthAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:44381/api/Auth");
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory");
});

builder.Services.AddMvc()
    .AddSessionStateTempDataProvider();
builder.Services.AddSession();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region File Upload and Request Path
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "mvc/server/uploads")),
    RequestPath = "/images/mvc/server/uploads"
});
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.UseSession();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
