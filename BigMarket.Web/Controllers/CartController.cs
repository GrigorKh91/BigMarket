using BigMarket.Web.Models;
using BigMarket.Web.Models.ShoppingCartAPI;
using BigMarket.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace BigMarket.Web.Controllers
{
    public class CartController(ICartService cartService) : Controller
    {
        private readonly ICartService _cartService = cartService;

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            var cartDto = await LoadCartDtoBaseOnLoggedInUser();
            return View(cartDto);
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
