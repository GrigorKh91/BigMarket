using AutoMapper;
using BigMarket.Services.OrderAPI.Data;
using BigMarket.Services.OrderAPI.Models;
using BigMarket.Services.OrderAPI.Models.Dto;
using BigMarket.Services.OrderAPI.Services.IServices;
using BigMarket.Services.OrderAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigMarket.Services.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController(IMapper mapper,
                                                        AppDbContext db,
                                                        IProductService productService) : ControllerBase
    {
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _db = db;
        private readonly IProductService _productService = productService;


        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto =_mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;
                orderHeaderDto.OrderDetalis = _mapper.Map<IEnumerable<OrderDetalisDto>>(cartDto.CartDetalis);
                OrderHeader orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
                 await _db.OrderHeaders.AddAsync(orderHeader);
                await _db.SaveChangesAsync();
                orderHeaderDto.OrderHeaderId = orderHeader.OrderHeaderId;
                _response.Result = orderHeaderDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;   
        }



    }
}
