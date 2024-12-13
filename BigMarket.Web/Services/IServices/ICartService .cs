﻿using BigMarket.Web.Models;
using BigMarket.Web.Models.ShoppingCartAPI;

namespace BigMarket.Web.Services.IServices
{
    public interface ICartService
    {
        Task<ResponseDto> GetcartByUserIdAsync(string userId);
        Task<ResponseDto> UpsertCartasync(CartDto cartDto);
        Task<ResponseDto> RemoveFromCartAsync(int cartDetalisId);
        Task<ResponseDto> ApplayCoupontAsync(CartDto cartDto);
        Task<ResponseDto> EmailCartAsync(CartDto cartDto);
    }
}
