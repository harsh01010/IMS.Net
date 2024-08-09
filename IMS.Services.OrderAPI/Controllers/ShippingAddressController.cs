using IMS.Services.OrderAPI.Models.DTO;
using IMS.Services.OrderAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Services.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShippingAddressController : ControllerBase
    {

        private readonly IShippingAddressRepository shippingAddressRepository;

        protected ResponseDto responseDto { get; set; }

        public ShippingAddressController(IShippingAddressRepository shippingAddressRepository)
        {
            this.shippingAddressRepository = shippingAddressRepository;
            responseDto = new ResponseDto();
        }

        [HttpPost]
        [Route("{userId:Guid}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<IActionResult> AddAddress([FromRoute] Guid userId, [FromBody] AddAddressRequestDto addAddressRequestDto)
        {
            var responseString = await shippingAddressRepository.AddAddressAsync(userId, addAddressRequestDto);

            if (string.IsNullOrEmpty(responseString))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Something Went Wrong";
                return BadRequest(responseDto);

            }
            responseDto.IsSuccess = true;
            responseDto.Message = responseString;
            return Ok(responseDto);
        }
        [HttpDelete]
        [Route("{shippingAddressId:Guid}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid shippingAddressId)
        {
            var responseString = await shippingAddressRepository.DeleteAddressAsync(shippingAddressId);
            if (String.IsNullOrEmpty(responseString))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Something Went Wrong";
                return BadRequest(responseDto);
            }
            responseDto.IsSuccess = true;
            responseDto.Message = responseString;
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllAddress([FromRoute]Guid userId)
        {
            var listOfAddressesForUser = await shippingAddressRepository.GetAllAddressAsync(userId);
            responseDto.IsSuccess = true;
            responseDto.Message = "Fetched Successfully";
            responseDto.Result = listOfAddressesForUser;
            return Ok(responseDto);
        }

    }
}
