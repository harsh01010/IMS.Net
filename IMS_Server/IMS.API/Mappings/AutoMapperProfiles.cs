﻿using AutoMapper;
using IMS.API.Models.Dto.Product;
using IMS.API.Models.Dto.ShippingAddress;
using IMS.API.Models.Domain.Product;
using IMS.API.Models.Domain.ShippingAddress;

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
            }

          
        }
    }

