using AutoMapper;
using IMS.API.Repository.IRepository.IProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IMS.API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using IMS.API.Models.Domain.Product;
using IMS.API.Models.Dto.Product;

namespace IMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private ResponseDto response;

        public ProductAPIController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.response = new ResponseDto();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<ResponseDto> GetAll()
        {
            try
            {
                List<ProductModel> result = await productRepository.GetAllAsync();
                response.IsSuccess = true;
                response.Message = "Product(s) retrieved successfully";
                response.Result = mapper.Map<List<ProductDto>>(result);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<ResponseDto> GetById(Guid id)
        {
            try
            {
                ProductModel? result = await productRepository.GetByIdAsync(id);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Product retrieved successfully";
                    response.Result = mapper.Map<ProductDto>(result);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDto> Create([FromBody] CreateRequestDto createRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid model state";
                    response.Result = ModelState;
                }
                else
                {
                    ProductModel product = mapper.Map<ProductModel>(createRequestDto);
                    product = await productRepository.CreateAsync(product);
                    response.IsSuccess = true;
                    response.Message = "Product created successfully";
                    response.Result = mapper.Map<ProductDto>(product);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDto> Update(Guid id, [FromBody] UpdateRequestDto updateRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid model state";
                    response.Result = ModelState;
                }
                else
                {
                    ProductModel product = mapper.Map<ProductModel>(updateRequestDto);
                    product.ProductId = id;
                    product = await productRepository.UpdateAsync(product);
                    response.IsSuccess = true;
                    response.Message = "Product updated successfully";
                    response.Result = mapper.Map<ProductDto>(product);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDto> Delete(Guid id)
        {
            try
            {
                ProductModel? result = await productRepository.DeleteAsync(id);
                if (result == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Product deleted successfully";
                    response.Result = mapper.Map<ProductDto>(result);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
