using IMS.API.Models.Dto;
using IMS.API.Models.Dto.ShoppingCart;
using IMS.API.Repository.IRepository.IShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS.API.Controllers
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
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Upsert([FromRoute] Guid cartId, [FromBody] CartRequestDto requestDto)
        {


            var responseString = await cartRepository.UpsertAsync(cartId, requestDto.ProductId);

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
      //  [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> DeleteProductFromCart([FromRoute] Guid cartId, [FromBody] CartRequestDto requestDto)
        {
            var responseMessage = await cartRepository.DeleteProductFromCartAsync(cartId, requestDto.ProductId);

            if (String.IsNullOrEmpty(responseMessage))
            {
                responseDto.IsSuccess = false;
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
     //   [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllPrductsInCart([FromRoute] Guid cartId)
        {
            

            var res = await cartRepository.GetCartAsync(cartId);
            if (res.Products != null)
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
    //    [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> EmailCart([FromRoute] Guid cartId)
        {
            //fetch the cart

            var res = await cartRepository.GetCartAsync(cartId);
            if (res.Products != null)
            {
                var resString = await cartRepository.SendCartByEmailAsync(res);
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

        [HttpDelete("deleteCart/{cartId:guid}")]
       // [Authorize(Roles ="Admin,Customer")]

        public async Task<IActionResult> DeleteCart([FromRoute] Guid cartId)
        {
            var res = await cartRepository.DeleteCartAsync(cartId);
            if(res)
            {
                responseDto.IsSuccess = true;
                responseDto.Message = "Deleted the cart";
                return Ok(responseDto);
            }
            return NotFound(responseDto);

        }


    }
}
