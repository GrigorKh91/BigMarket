using BigMarket.Web.Models;
using BigMarket.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using BigMarket.Web.Models.ProductAPI;
using Microsoft.AspNetCore.Authorization;
using BigMarket.Web.Models.ShoppingCartAPI;
using IdentityModel;
using BigMarket.Web.Utility;

namespace BigMarket.Web.Controllers
{
    public class HomeController(IProductService productService,
                                                  ICartService cartService) : Controller
    {
        private readonly IProductService _productService = productService;
        private readonly ICartService _cartService = cartService;

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = [];
            ResponseDto response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData[MessageType.Error] = response?.Message;
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> ProductDetalis(int productId)
        {
            ProductDto productDto = null;
            ResponseDto response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData[MessageType.Error] = response?.Message;
            }

            return View(productDto);
        }

        [Authorize]
        [HttpPost]
        [ActionName("ProductDetalis")]
        public async Task<IActionResult> ProductDetalis(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };
            CartDetalisDto cartDetalisDto = new ()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };
            List<CartDetalisDto> cartDetalisDtos = [cartDetalisDto];
            cartDto.CartDetalis = cartDetalisDtos;

            ResponseDto response = await _cartService.UpsertCartasync(cartDto);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Item has been added to the Shopping Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData[MessageType.Error] = response?.Message;
            }

            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
