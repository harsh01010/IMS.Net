using AutoMapper;
using IMS.API.Models.Dto.Product;
using IMS.API.Models.Dto.ShippingAddress;
using IMS.API.Models.Domain.Product;
using IMS.API.Models.Domain.ShippingAddress;
using IMS.API.Models.Dto.Order;
using IMS.API.Models.Domain.Order;

namespace IMS.API.Mappings
{
   
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ProductModel, ProductDto>().ReverseMap();
                CreateMap<ProductModel, CreateRequestDto>().ReverseMap();
                CreateMap<ProductModel, UpdateRequestDto>().ReverseMap();
                CreateMap<AddAddressRequestDto, ShippingAddressModel>().ReverseMap();
                CreateMap<ShippingAddressModel,ReturnShippingAddressDto>();
                CreateMap<OrderModel,OrderDetailsDto>().ReverseMap();
                CreateMap<CategoryModel,ReturnCategoryDto>().ReverseMap();
                CreateMap<CategoryModel,AddCategoryDto>().ReverseMap();
            }

          
        }
    }

