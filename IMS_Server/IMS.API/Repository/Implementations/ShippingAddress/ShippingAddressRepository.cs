using AutoMapper;
using IMS.API.Models.Dto.ShippingAddress;
using IMS.API.Repository.IRepository;
using IMS.API.Repository.IRepository.IShippingAddress;
using IMS.API.Data;
using IMS.API.Models.Domain.ShippingAddress;
using Microsoft.EntityFrameworkCore;

namespace IMS.API.Repository.Implementations.ShippingAddress
{
    public class ShippingAddressRepository:IShippingAddressRepository
    {

        private readonly IMapper mapper;
        private readonly IMSDbContext db;

        public ShippingAddressRepository(IMapper mapper, IMSDbContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }
        public async Task<string> AddAddressAsync(Guid userId, AddAddressRequestDto addAddressRequestDto)
        {
            var addressDomainModel = mapper.Map<ShippingAddressModel>(addAddressRequestDto);
            addressDomainModel.userId = userId;

            if (userId != null)
            {
                await db.ShippingAddresses.AddAsync(addressDomainModel);

                await  db.SaveChangesAsync();
                return "Address Added Successfully";
            }
            return "";



        }

        public async Task<string> DeleteAddressAsync(Guid shippingAddressId)
        {
            var shippingAddress = await db.ShippingAddresses.FirstOrDefaultAsync(y => y.shippingAddressId == shippingAddressId);

            if (shippingAddress != null)
            {
                db.ShippingAddresses.Remove(shippingAddress);
                await db.SaveChangesAsync();
                return "Address Deleted Successfully";
            }
            return "";
        }

        public async Task<List<ReturnShippingAddressDto>> GetAllAddressAsync(Guid userId)
        {
            var shippingAllAddress = await db.ShippingAddresses.Where(x => x.userId == userId).ToListAsync();
            //return shippingAllAddress
            return mapper.Map<List<ReturnShippingAddressDto>>(shippingAllAddress);
        }

        public async Task<ReturnShippingAddressDto> GetShippingAddressAsync(Guid shippingAddressId)
        {
            try
            {
                var shippingAddress = await db.ShippingAddresses.FirstOrDefaultAsync(curr=>curr.shippingAddressId==shippingAddressId);
                return mapper.Map<ReturnShippingAddressDto>(shippingAddress);
            }
            catch
            {
                return null;
            }
           
        }
    }
}
