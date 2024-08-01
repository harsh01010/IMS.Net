using IMS.Services.ShoppingCartAPI.Models.Dto;
using IMS.Services.ShoppingCartAPI.Data;
using IMS.Services.ShoppingCartAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        protected ResponseDto responseDto;
        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
            responseDto = new ResponseDto();
        }


        [HttpPost]
        [Route("upsert/{cartId:Guid}")]
        public async Task<IActionResult> Upsert([FromRoute] Guid cartId, [FromBody] RequestDto requestDto)
        {
            var responseString = await cartRepository.UpsertAsync(cartId,  requestDto.ProductId);

            if (!String.IsNullOrEmpty(responseString))
            {



                responseDto.IsSuccess = true;
                responseDto.Message = responseString;
                return Ok(responseDto);
            }

            else
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "something went wrong";
                return BadRequest(responseDto);

            }
        }

        [HttpDelete]
        [Route("delete/{cartId:Guid}")]
        public async Task<IActionResult> DeleteProductFromCart([FromRoute]Guid cartId, [FromBody] RequestDto requestDto)
        {
            var responseMessage = await cartRepository.DeleteProductFromCartAsync(cartId, requestDto.ProductId);

            if(String.IsNullOrEmpty(responseMessage))
            {
                responseDto.IsSuccess= false;
                responseDto.Message = "Something went wrong,Please try again";
                return BadRequest(responseDto);
            }
            else
            {
                responseDto.IsSuccess = true;
                responseDto.Message = responseMessage;
                return Ok(responseDto);
            }
        }

        [HttpGet]
        [Route("get/{cartId:Guid}")]
        public async Task<IActionResult> GetAllPrductsInCart([FromRoute]Guid cartId)
        {
            var res = await cartRepository.GetCartAsync(cartId);
            if(res.Products!=null)
            {
                responseDto.IsSuccess = true;
                responseDto.Message = "cart fetched successfuly";
                responseDto.Result = res;
                return Ok(responseDto);
            }
            else
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "cart does not exist";
                return BadRequest(responseDto);
            }
            
        }

    }
}
