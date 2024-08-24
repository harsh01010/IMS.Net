using AutoMapper;
using Dapper;
using IMS.API.Data;
using IMS.API.Models.Domain.ShippingAddress;
using IMS.API.Models.Dto.ShippingAddress;
using IMS.API.Repository.IRepository.IShippingAddress;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.OrderAPI.Repository
{
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        private readonly IMapper mapper;
      
        private readonly string connectionString;

        public ShippingAddressRepository(IMapper mapper, IMSDbContext DbContext )
        {
            this.mapper = mapper;
            this.DbContext = DbContext;
            this.connectionString = DbContext.Database.GetDbConnection().ConnectionString;
        }

        public IMSDbContext IMSDbContext { get; }
        public IMSDbContext DbContext { get; }

        public async Task<string> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto)
        {
            var addressDomainModel = mapper.Map<ShippingAddressModel>(addAddressRequestDto);
            addressDomainModel.userId = userId;

            if (userId != Guid.Empty)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var query = @"
                        INSERT INTO ShippingAddresses (ShippingAddressId, UserId, houseNo,street, state, city, pinCode)
                        VALUES (@ShippingAddressId, @UserId, @houseNo,@street, @state, @city, @pinCode)";

                    await connection.ExecuteAsync(query, new
                    {
                        addressDomainModel.shippingAddressId,
                        addressDomainModel.userId,
                        addressDomainModel.houseNo,
                        addressDomainModel.street,
                        addressDomainModel.state,
                        addressDomainModel.city,
                        addressDomainModel.pinCode,
                    });
                }
                return "Address Added Successfully";
            }
            return "Invalid User ID";
        }

        public async Task<string> DeleteAddressAsync(Guid shippingAddressId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM ShippingAddresses WHERE ShippingAddressId = @ShippingAddressId";
                var rowsAffected = await connection.ExecuteAsync(query, new { ShippingAddressId = shippingAddressId });

                if (rowsAffected > 0)
                {
                    return "Address Deleted Successfully";
                }
            }
            return "Address Not Found";
        }

        public async Task<List<ReturnShippingAddressDto>> GetAllAddressAsync(Guid userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM ShippingAddresses WHERE UserId = @UserId";
                var shippingAllAddress = await connection.QueryAsync<ShippingAddressModel>(query, new { UserId = userId });

                return mapper.Map<List<ReturnShippingAddressDto>>(shippingAllAddress);
            }
        }

        public Task<ReturnShippingAddressDto> GetShippingAddressAsync(Guid shippingAddressId)
        {
            throw new NotImplementedException();
        }
    }
}
