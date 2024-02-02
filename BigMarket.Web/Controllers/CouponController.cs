using BigMarket.Web.Models;
using BigMarket.Web.Models.CouponApi;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BigMarket.Web.Controllers
{
    public class CouponController(ICouponService couponService) : Controller
    {
        private readonly ICouponService _couponService = couponService;

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto> list = [];
            ResponseDto response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData[MessageType.Error] = response?.Message;
            }

            return View(list);
        }


        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto response = await _couponService.CreateCouponAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData[MessageType.Success] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData[MessageType.Error] = response?.Message;
                }
            }
            return View(model);
        }


        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto response = await _couponService.GetCouponByIdAsync(couponId);
            if (response != null && response.IsSuccess)
            {
                CouponDto model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData[MessageType.Error] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto model)
        {
            ResponseDto response = await _couponService.DeleteCouponAsync(model.CouponId);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData[MessageType.Error] = response?.Message;
            }
            return View(model);
        }
    }
}
