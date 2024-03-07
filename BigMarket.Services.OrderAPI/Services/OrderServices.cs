using AutoMapper;
using BigMarket.MessageBus;
using BigMarket.Services.OrderAPI.Data;
using BigMarket.Services.OrderAPI.Models;
using BigMarket.Services.OrderAPI.Models.Dto;
using BigMarket.Services.OrderAPI.Services.IServices;
using BigMarket.Services.OrderAPI.Utility;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace BigMarket.Services.OrderAPI.Services
{
    public class OrderServices(IMapper mapper,
                                                AppDbContext db,
                                                IMessageBus messageBus,
                                                IStripeService stripeService,
                                                IConfiguration configuration) : IOrderService
    {
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _db = db;
        private readonly IMessageBus _messageBus = messageBus;
        private readonly IStripeService _stripeService = stripeService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ResponseDto> GetByUserIdAsync(bool isAdmin, string userId = "")
        {
            try
            {
                IEnumerable<OrderHeader> objList;
                if (isAdmin)
                {
                    objList = await _db.OrderHeaders.Include(u => u.OrderDetalis)
                                                                          .OrderByDescending(u => u.OrderHeaderId)
                                                                          .ToListAsync();
                }
                else
                {
                    objList = await _db.OrderHeaders.Include(u => u.OrderDetalis)
                                                                          .Where(u => u.UserId == userId)
                                                                         .OrderByDescending(u => u.OrderHeaderId)
                                                                         .ToListAsync();
                }
                _response.Result = _mapper.Map<IEnumerable<OrderHeaderDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public async Task<ResponseDto> GetByOrderIdAsync(int id)
        {
            try
            {
                OrderHeader orderHeader = await _db.OrderHeaders.Include(u => u.OrderDetalis)
                                                                                                      .FirstAsync(u => u.OrderHeaderId == id);
                _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public async Task<ResponseDto> CreateAsync(CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
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
        public async Task<ResponseDto> UpdateStatusAsync(int orderId, string newStatus)
        {
            try
            {
                OrderHeader orderHeader = await _db.OrderHeaders.FirstAsync(u => u.OrderHeaderId == orderId);
                if (orderHeader != null)
                {
                    if (newStatus == SD.Status_Canceled)
                    {
                        var option = new RefundCreateOptions
                        {
                            Reason = RefundReasons.RequestedByCustomer,
                            PaymentIntent = orderHeader.PaymentIntentId
                        };

                        var service = new RefundService();
                        Refund refund = service.Create(option);
                    }
                }
                orderHeader.Status = newStatus;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }


        public async Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            try
            {
                _stripeService.CreateStripeSession(stripeRequestDto);
                OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId ==
                                                                       stripeRequestDto.OrderHeader.OrderHeaderId);
                orderHeader.StripeSessionId = stripeRequestDto.SessionId;
                await _db.SaveChangesAsync();
                _response.Result = stripeRequestDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public async Task<ResponseDto> ValidateStripeSession(int orderHeaderId)
        {
            try
            {
                OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == orderHeaderId);
                var paymentIntentInfo = _stripeService.GetPaymentIntentInfo(orderHeader.StripeSessionId);

                if (paymentIntentInfo.Status == "succeeded") // TODO should to refactor
                {
                    orderHeader.PaymentIntentId = paymentIntentInfo.Id;
                    orderHeader.Status = SD.Status_Approved;
                    await _db.SaveChangesAsync();
                    RewardsDto rewardsDto = new()
                    {
                        OrderId = orderHeader.OrderHeaderId,
                        RewardsActivity = Convert.ToInt32(orderHeader.OrderTotal),
                        UserId = orderHeader.UserId
                    };
                    string topicname = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
                    await _messageBus.PublishMessageAsync(rewardsDto, topicname);
                    _response.Result = _mapper.Map<OrderHeaderDto>(orderHeader);
                }

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
