using IMS.Services.ShoppingCartAPI.Models.Dto;
using IMS.Services.ShoppingCartAPI.Data;
using IMS.Services.ShoppingCartAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IMS.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles ="Admin,Customer")]
        public async Task<IActionResult> Upsert([FromRoute] Guid cartId, [FromBody] RequestDto requestDto)
        {

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var responseString = await cartRepository.UpsertAsync(cartId,  requestDto.ProductId,token);

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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> DeleteProductFromCart([FromRoute]Guid cartId, [FromBody] RequestDto requestDto)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var responseMessage = await cartRepository.DeleteProductFromCartAsync(cartId, requestDto.ProductId,token);

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
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllPrductsInCart([FromRoute]Guid cartId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
       
            var res = await cartRepository.GetCartAsync(cartId,token);
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

        [HttpPost("emailCart/{cartId:Guid}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<IActionResult> EmailCart([FromRoute]Guid cartId)
        {
            //fetch the cart
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var res = await cartRepository.GetCartAsync(cartId, token);
            if (res.Products != null)
            {
                var resString = await cartRepository.SendCartByEmailAsync(res, token);
                if (!String.IsNullOrEmpty(resString))
                {
                    responseDto.IsSuccess = true;
                    responseDto.Message = resString;
                    return Ok(responseDto);
                }
               
               
            }
          
                responseDto.IsSuccess = false;
                responseDto.Message = "Something Went Wrong";
                return BadRequest(responseDto);


        }
    }
}
