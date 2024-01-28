using AutoMapper;
using BigMarket.Services.ShoppingCartAPI.Data;
using BigMarket.Services.ShoppingCartAPI.Models;
using BigMarket.Services.ShoppingCartAPI.Models.Dto;
using BigMarket.Services.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigMarket.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController(IMapper mapper,
                                             AppDbContext db,
                                             IProductService productService,
                                             ICouponService couponService) : ControllerBase
    {
        private readonly ResponseDto _response = new();
        private readonly IMapper _mapper = mapper;
        private readonly AppDbContext _db = db;
        private readonly IProductService _productService = productService;
        private readonly ICouponService _couponService = couponService;

        [HttpGet("GetCart/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartHeader cartHeaderFromDb = _db.CartHeaders.First(u => u.UserId == userId);
                IEnumerable<CartDetalis> cartDetalisListFromDb = _db.CartDetalis
                    .Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(cartHeaderFromDb),
                    CartDetalis = _mapper.Map<IEnumerable<CartDetalisDto>>(cartDetalisListFromDb)
                };

                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();

                foreach (var item in cart.CartDetalis)
                {
                    item.Product = productDtos.FirstOrDefault(p => p.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                // applay coupon if any
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("ApplayCoupon")]
        public async Task<ResponseDto> ApplayCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders
                    .FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);

                cartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("CartUpsent")]
        public async Task<ResponseDto> CartUpsent(CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId ==
                                                                                                        cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //  create header and detalis
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDto.CartDetalis.First().CartHeaderId = cartHeader.CartHeaderId;
                    CartDetalis cartDetalis = _mapper.Map<CartDetalis>(cartDto.CartDetalis.First());
                    await _db.CartDetalis.AddAsync(cartDetalis);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // if header is not null
                    // check if detalis has same productid
                    var cartDetalisFromDb = await _db.CartDetalis.AsNoTracking().FirstOrDefaultAsync(
                           u => u.ProductId == cartDto.CartDetalis.First().ProductId &&
                           u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetalisFromDb == null)
                    {
                        // create cartdetalis
                        cartDto.CartDetalis.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        CartDetalis cartDetalis = _mapper.Map<CartDetalis>(cartDto.CartDetalis.First());
                        await _db.CartDetalis.AddAsync(cartDetalis);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        // update count in cart detalis
                        cartDto.CartDetalis.First().Count += cartDetalisFromDb.Count;
                        cartDto.CartDetalis.First().CartHeaderId = cartDetalisFromDb.CartHeaderId;
                        cartDto.CartDetalis.First().CartDetalisId = cartDetalisFromDb.CartDetalisId;
                        CartDetalis cartDetalis = _mapper.Map<CartDetalis>(cartDto.CartDetalis.First());
                        _db.CartDetalis.Update(cartDetalis);
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetalisId)
        {
            try
            {
                CartDetalis cartDetalis = _db.CartDetalis.First(u => u.CartDetalisId == cartDetalisId);
                int totalcountOfCartItem = _db.CartDetalis.Where(u => u.CartHeaderId == cartDetalis.CartHeaderId).Count();
                _db.CartDetalis.Remove(cartDetalis);
                if (totalcountOfCartItem == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetalis.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
