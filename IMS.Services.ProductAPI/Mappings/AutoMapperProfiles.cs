using AutoMapper;
using IMS.Services.ProductAPI.Models;
using IMS.Services.ProductAPI.Models.Dto;
using static Azure.Core.HttpHeader;

namespace IMS.Services.ProductAPI.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Product, CreateRequestDto>().ReverseMap();
			CreateMap<Product, UpdateRequestDto>().ReverseMap();
		}
	}
}
