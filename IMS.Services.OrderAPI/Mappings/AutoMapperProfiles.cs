using AutoMapper;
using IMS.Services.OrderAPI.Models.Domain;
using IMS.Services.OrderAPI.Models.DTO;

namespace IMS.Services.OrderAPI.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        

        public AutoMapperProfiles() {
            CreateMap<ShippingAddress, ReturnShippingAddressDto>().ReverseMap();
            CreateMap<ShippingAddress,AddAddressRequestDto>().ReverseMap();
            CreateMap<Order,OrderDetailsDto>().ReverseMap();    
        }
    }
}
