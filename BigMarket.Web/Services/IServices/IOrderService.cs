﻿using BigMarket.Web.Models;
using BigMarket.Web.Models.ShoppingCartAPI;

namespace BigMarket.Web.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto> CreateOrder(CartDto cartDto);
        Task<ResponseDto> CreateStripeSession(StripeRequestDto stripeRequestDto);
        Task<ResponseDto> ValidateStripeSession(int orderHeaderId);
        Task<ResponseDto> GetAllOrders(string userId);
        Task<ResponseDto> GetOrder(int orderId);
        Task<ResponseDto> UpdateOrderStatus(int orderId, string newStatus);
    }
}