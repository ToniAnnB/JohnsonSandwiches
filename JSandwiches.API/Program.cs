using AutoMapper;
using JSandwiches.API.IRespository;
using JSandwiches.API.Models;
using JSandwiches.API.Respository;
using JSandwiches.Configurations;
using JSandwiches.Models.Data;
using JSandwiches.Services;
using JSandwiches.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


#region Server Connection and Identity Config

//Connects to server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"), b => b.MigrationsAssembly("JSandwiches.API"));
});
// 

//Configuring the Identity system to the application's services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

#endregion


#region Transient Services
// registering the AuthService and UnitOfWork class with the dependency injection (registers the service with a transient lifetime)
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
#endregion


#region JWTokenBearer Config
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JWTConfig:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("JWTConfig:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTConfig:Key").Value))
    };
});
#endregion



//builder.Services.AddResponseCaching();


//adding automapper for modelDTO mapping
builder.Services.AddAutoMapper(typeof(MappingConfig));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Serilog Configuration
Log.Logger = new LoggerConfiguration()
             .WriteTo.File(path: "c:\\JSandwhiches\\logs\\log-txt",
             outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {NewLine} {Message:1j}{NewLine}{Exception}",
             rollingInterval: RollingInterval.Day,
             restrictedToMinimumLevel: LogEventLevel.Information
             ).CreateLogger();


builder.Host.UseSerilog();
#endregion


var app = builder.Build();

#region Global Error Handling
app.UseExceptionHandler(error => {
    error.Run(async context => {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            Log.Error($"Something went wrong in {contextFeature.Error}");
            await context.Response.WriteAsync(new Error
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Serever Error. Please Try Again Later."
            }.ToString());
        }
    });
});
#endregion


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await SeedData.Intialize(serviceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

