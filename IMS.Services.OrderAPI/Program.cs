using IMS.Services.OrderAPI.Data;
using IMS.Services.OrderAPI.Mappings;
using IMS.Services.OrderAPI.Repository;
using IMS.Services.OrderAPI.Repository.IRepository;
using IMS.Services.OrderAPI.Service;
using IMS.Services.OrderAPI.Service.IService;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<OrderDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddDbContext<ShippingAddressDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShippingAddressRepository, ShippingAddressRepository>();

builder.Services.AddHttpClient<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
