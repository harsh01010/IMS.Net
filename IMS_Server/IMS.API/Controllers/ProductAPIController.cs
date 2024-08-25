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

        [HttpGet]
        [Route("getAllCategories")]
        public async Task<ResponseDto> GetAllCategories()
        {
            try
            {
                var categories = await productRepository.GetAllCategoriesAsync();

                response.IsSuccess = true;
                response.Result = mapper.Map<List<ReturnCategoryDto>>(categories);
                response.Message = "Fetched categories successfully";
                
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "something went wrong";
            }
            return response;

        }


        [HttpGet]
        [Route("getProductPage")]
        public async Task<ResponseDto> GetProductPage([FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            GetPageRequestDto pageRequestDto = new GetPageRequestDto { PageNum = pageNum, PageSize = pageSize };
            try
            {
                var res = await productRepository.GetProductPageAsync(pageRequestDto);
                if (res.Any())
                {
                    response.IsSuccess = true;
                    response.Message = "Fetched Products Successfully";
                    response.Result = mapper.Map<List<ProductDto>>(res);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No Products available";
                    response.Result = mapper.Map<List<ProductDto>>(res);
                }

            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "Failed to load";

            }
            return response;
        }
        [HttpGet]
        [Route("getPruductsByCategoryId/{categoryId:Guid}")]
        public async Task<ResponseDto> GetPruductsByCategoryId([FromRoute] Guid categoryId)
        {
            try
            {
                var products = await productRepository.GetAllProductsByCategoryId(categoryId);
                response.IsSuccess = true;
                response.Result = mapper.Map<List<ProductDto>>(products);
                response.Message = "Fetched Products successfully";
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }
            return response;
        }

        [HttpPost]
        [Route("addNewCategory")]
        public async Task<ResponseDto> AddNewCategory([FromBody] AddCategoryDto category)
        {
            try
            {
                var res = await productRepository.AddNewCategoryAsync(category);
                if (res)
                {
                    response.IsSuccess = true;
                    response.Message = $"Added {category.CategoryName}";
                }
                else
                {
                    response.IsSuccess= false;
                    response.Message = "Failed to add";
                }

            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "Failed to add";
            }

            return response;

        }

       
    }
}
