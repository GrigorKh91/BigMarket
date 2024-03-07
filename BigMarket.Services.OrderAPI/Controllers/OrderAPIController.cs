using BigMarket.Services.OrderAPI.Models.Dto;
using BigMarket.Services.OrderAPI.Services.IServices;
using BigMarket.Services.OrderAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<ResponseDto> Get(string userId = "")
        {
            var isAdmin = User.IsInRole(SD.Role_Admin);
            ResponseDto _response = await _orderService.GetByUserIdAsync(isAdmin, userId);
            return _response;
        }

        [Authorize]
        [HttpGet("GetOrder/{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto _response = await _orderService.GetByOrderIdAsync(id);
            return _response;
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
        {
            ResponseDto _response = await _orderService.CreateAsync(cartDto);
            return _response;
        }

        [Authorize]
        [HttpPost("CreateStripeSession")]
        public async Task<ResponseDto> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            ResponseDto _response = await _orderService.CreateStripeSession(stripeRequestDto);
            return _response;
        }

        [Authorize]
        [HttpPost("ValidateStripeSession")]
        public async Task<ResponseDto> ValidateStripeSession([FromBody] int orderHeaderId)
        {
            ResponseDto _response = await _orderService.ValidateStripeSession(orderHeaderId);
            return _response;
        }

        [Authorize]
        [HttpPost("UpdateOrderStatus/{orderId:int}")]
        public async Task<ResponseDto> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            ResponseDto _response = await _orderService.UpdateStatusAsync(orderId, newStatus);
            return _response;
        }
    }
}
