﻿using BigMarket.Services.ShoppingCartAPI.Models.Dto;

namespace BigMarket.Services.ShoppingCartAPI.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon( string couponCode);
    }
}
