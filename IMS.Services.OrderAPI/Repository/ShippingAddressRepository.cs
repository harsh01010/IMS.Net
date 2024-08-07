using AutoMapper;
using IMS.Services.OrderAPI.Data;
using IMS.Services.OrderAPI.Models.Domain;
using IMS.Services.OrderAPI.Models.DTO;
using IMS.Services.OrderAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.OrderAPI.Repository
{
    public class ShippingAddressRepository : IShippingAddressRepository
    {
        private readonly IMapper mapper;
        private readonly ShippingAddressDbContext shippingAddressDbContext;

        public ShippingAddressRepository(IMapper mapper,ShippingAddressDbContext shippingAddressDbContext)
        {
            this.mapper = mapper;
            this.shippingAddressDbContext = shippingAddressDbContext;
        }
        public async Task<string> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto)
        {
            var addressDomainModel = mapper.Map<ShippingAddress>(addAddressRequestDto);
            addressDomainModel.userId = userId;

            if(userId != null)
            {
                await shippingAddressDbContext.ShippingAddresses.AddAsync(addressDomainModel);

                await shippingAddressDbContext.SaveChangesAsync();
                return "Address Added Successfully";
            }
            return ""; 


            
        }

        public async Task<string> DeleteAddressAsync(Guid shippingAddressId)
        {
              var shippingAddress=await shippingAddressDbContext.ShippingAddresses.FirstOrDefaultAsync(y=> y.shippingAddressId == shippingAddressId);

            if (shippingAddress != null)
            {
                shippingAddressDbContext.ShippingAddresses.Remove(shippingAddress);
                await shippingAddressDbContext.SaveChangesAsync();
                return "Address Deleted Successfully";
            }
            return "";
        }

        public async Task<List<ReturnShippingAddressDto>> GetAllAddressAsync(Guid userId)
        {
            var shippingAllAddress=await shippingAddressDbContext.ShippingAddresses.Where(x=>x.userId==userId).ToListAsync();
            //return shippingAllAddress
            return mapper.Map<List<ReturnShippingAddressDto>>(shippingAllAddress);
        }
    }
}
