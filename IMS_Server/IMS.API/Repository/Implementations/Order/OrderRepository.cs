using AutoMapper;
using Dapper;
using IMS.API.Models.Dto.Order;
using IMS.API.Models.Dto;
using IMS.API.Repository.IRepository.IOrder;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using IMS.API.Models.Domain.ShippingAddress;
using IMS.API.Data;
using IMS.API.Models.Domain.Order;
using IMS.API.Repository.IRepository.IAuth;
using IMS.API.Models.Dto.Auth;
using IMS.API.Repository.IRepository.IShoppingCart;

namespace IMS.Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        
        private readonly string orderDbConnectionString;
        private readonly string shippingAddressDbConnectionString;
        private readonly IMSAuthDbContext authDbContext;
        private readonly IMSDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAuthRepository authRepository;
        private readonly IEmailSender emailSender;
        private readonly ICartRepository cartRepository;

        public OrderRepository(IMSAuthDbContext authDbContext,IMSDbContext dbContext, IMapper mapper, IAuthRepository authRepository, IEmailSender emailSender, ICartRepository 
            cartRepository)
        {
       
            this.orderDbConnectionString = dbContext.Database.GetDbConnection().ConnectionString;
            this.shippingAddressDbConnectionString = dbContext.Database.GetDbConnection().ConnectionString;
          
            this.authDbContext = authDbContext;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.authRepository = authRepository;
            this.emailSender = emailSender;
            this.cartRepository = cartRepository;
        }

        public async Task<string> PlaceOrderAsync(Guid cartId, Guid shippingAddressId, string token = null)
        {
            try
            {
                // fetch the cart
                var cart = await cartRepository.GetCartAsync(cartId, token);

                // fetch the address
                ShippingAddressModel shippingAddress;
                using (var connection = new SqlConnection(shippingAddressDbConnectionString))
                {
                    shippingAddress = await connection.QueryFirstOrDefaultAsync<ShippingAddressModel>(
                        "SELECT * FROM ShippingAddresses WHERE shippingAddressId = @shippingAddressId",
                        new { shippingAddressId }
                    );
                }

                // payment
                bool payment = true;

                // insert into the orders table
                var order = new OrderModel { CustomerId = cartId, OrderTime = DateTime.Now, OrderValue = cart.TotalValue };
                if (cart.TotalValue != 0 && shippingAddress != null && payment)
                {
                    order.Status = true;
                }
                else
                {
                    order.Status = false;
                }

                using (var connection = new SqlConnection(orderDbConnectionString))
                {
                    var orderId = await connection.QuerySingleAsync<Guid>(
                        "INSERT INTO Orders (CustomerId, OrderTime, OrderValue, Status) OUTPUT INSERTED.OrderId VALUES (@CustomerId, @OrderTime, @OrderValue, @Status)",
                        order
                    );
                    order.OrderId = orderId;

                    // insert into the order items table
                    var products = cart.Products;
                    if (products.Any())
                    {
                        foreach (var product in products)
                        {
                            await connection.ExecuteAsync(
                                "INSERT INTO OrderItems (OrderId, ProductId, ProductCount) VALUES (@OrderId, @ProductId, @ProductCount)",
                                new { OrderId = order.OrderId, ProductId = product.ProductId, ProductCount = product.ProductCount }
                            );
                        }
                    }
                }

                 var user = await authRepository.GetById(cartId);
                if (user != null)
                {
                    var emailBody = $"Congratulations {user.Name} , your order with amount {cart.TotalValue} has been placed!";
                    var emailSub = "Order Confirmation";
                    var res = await emailSender.EmailSendAsync(new SendEmailRequestDto { Email = user.Email, Subject = emailSub, Body = emailBody });

                }


                var statusMessage = order.Status ? "placed Sucessfully" : "failed";


                return $"Order with id {order.OrderId} and value {order.OrderValue}, is  {statusMessage}";
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        public async Task<List<OrderDetailsDto>> GetAllOrdersAsync()
        {
            using (var connection = new SqlConnection(orderDbConnectionString))
            {
                var orders = await connection.QueryAsync<OrderModel>(
                    "SELECT * FROM Orders"
                );

                if (orders.Any())
                {
                    return mapper.Map<List<OrderDetailsDto>>(orders);
                }
                return new List<OrderDetailsDto>();
            }
        }
    }
}
