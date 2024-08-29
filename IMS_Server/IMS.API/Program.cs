using IMS.API.Data;
using IMS.API.Jobs;
using IMS.API.Mappings;
using IMS.API.Models.Domain.Auth;
using IMS.API.Repository.Implementations.Auth;
using IMS.API.Repository.Implementations.Order;
using IMS.API.Repository.Implementations.Product;
using IMS.API.Repository.Implementations.ShippingAddress;
using IMS.API.Repository.Implementations.ShoppingCart;
using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Repository.IRepository.IOrder;
using IMS.API.Repository.IRepository.IProduct;
using IMS.API.Repository.IRepository.IShippingAddress;
using IMS.API.Repository.IRepository.IShoppingCart;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Text;
using IMS.API;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<IMSAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddDbContext<IMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Add services to the container
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Register Quartz services
 builder.Services.AddInfrastructure();
//builder.Services.AddSingleton<IJobFactory, ScopedJobFactory>();
//builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
//builder.Services.AddQuartz(q =>
//{
//    q.UseMicrosoftDependencyInjectionJobFactory(); // Default job factory
//    q.UseSimpleTypeLoader();
//    q.UseInMemoryStore();
//    q.UseJobFactory<ScopedJobFactory>(); // Use the custom job factory
//});
//builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Setup authentication
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IMSAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[] { }
        }
    });
});

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Start Quartz Scheduler
//var scheduler = await app.Services.GetRequiredService<ISchedulerFactory>().GetScheduler();
//await scheduler.Start(CancellationToken.None);

app.Run();
