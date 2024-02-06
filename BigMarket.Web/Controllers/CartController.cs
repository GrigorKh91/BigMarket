using BigMarket.Web.Models;
using BigMarket.Web.Models.OrderAPI;
using BigMarket.Web.Models.ShoppingCartAPI;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace BigMarket.Web.Controllers
{
    public class CartController(ICartService cartService,
                                                IOrderService orderService) : Controller
    {
        private readonly ICartService _cartService = cartService;
        private readonly IOrderService _orderService = orderService;

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var cartDto = await LoadCartDtoBaseOnLoggedInUser();
            return View(cartDto);
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cartDto = await LoadCartDtoBaseOnLoggedInUser();
            return View(cartDto);
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBaseOnLoggedInUser();
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;

            var response = await _orderService.CreateOrder(cart);
            OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>
                (Convert.ToString(response.Result));
            if (response != null && response.IsSuccess)
            {
                // get stripe session and redirect to stripe to place order
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto,
                };
                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>
               (Convert.ToString(stripeResponse.Result));

                Response.Headers.Add("Location", stripeResponseResult.SessionUrl); //TODU check  
                return new StatusCodeResult(303);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDto response = await _orderService.ValidateStripeSession(orderId);
            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>
                    (Convert.ToString(response.Result));

                if (orderHeader != null && orderHeader.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
            }
            // TODO redirect to some error page
            return View(orderId);
        }


        public async Task<IActionResult> Remove(int cartDetalisId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?
                                                    .FirstOrDefault()?.Value; // TODO why need userId
            ResponseDto response = await _cartService.RemoveFromCartAsync(cartDetalisId);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View(); //TODO check View()
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto response = await _cartService.ApplayCoupontAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View(); //TODO check View()
        }

        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto) // TODO why need cartDto
        {
            CartDto cart = await LoadCartDtoBaseOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?
                                                   .FirstOrDefault()?.Value;
            ResponseDto response = await _cartService.EmailCartAsync(cart);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Email will be processed and sent shortly.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View(); //TODO check View()
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto response = await _cartService.ApplayCoupontAsync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View(); //TODO check View()
        }


        private async Task<CartDto> LoadCartDtoBaseOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?
                                                    .FirstOrDefault()?.Value;
            ResponseDto response = await _cartService.GetcartByUserIdAsync(userId);
            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new();
        }
    }
}
