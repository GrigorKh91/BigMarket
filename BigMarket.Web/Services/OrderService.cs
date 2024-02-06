﻿using BigMarket.Web.Models;
using BigMarket.Web.Models.CouponAPI;
using BigMarket.Web.Models.ShoppingCartAPI;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Services
{
    public sealed class OrderService(IBaseService baseService) : IOrderService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto> CreateOrder(CartDto cartDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = stripeRequestDto,
                Url = SD.OrderAPIBase + "/api/order/CreateStripeSession"
            };
            return await _baseService.SendAsync(request);
        }

        public async Task<ResponseDto> ValidateStripeSession(int orderHeaderId)
        {
            var request = new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = orderHeaderId,
                Url = SD.OrderAPIBase + "/api/order/ValidateStripeSession"
            };
            return await _baseService.SendAsync(request);
        }
    }
}
