using BigMarket.Web.Models;
using BigMarket.Web.Models.OrderAPI;
using BigMarket.Web.Services.IServices;
using BigMarket.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace BigMarket.Web.Controllers
{
    public class OrderController(IOrderService orderService) : Controller
    {
        private readonly IOrderService _orderService = orderService;
        public IActionResult OrderIndex()
        {
            return View();
        }

        public async Task<IActionResult> OrderDetalis(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.
                                                   FirstOrDefault()?.Value;
            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }
            if (!User.IsInRole(SD.RolaAdmin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }


        [HttpPost("OrderreadyForPickup")]
        public async Task<IActionResult> OrderreadyForPickup(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Status updated successfully";// TODO remove hard code
                return RedirectToAction(nameof(OrderDetalis), new { orderId });
            }

            return View();
        }


        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Status completed successfully";// TODO remove hard code
                return RedirectToAction(nameof(OrderDetalis), new { orderId });
            }

            return View();
        }


        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.Status_Canceled);
            if (response != null && response.IsSuccess)
            {
                TempData[MessageType.Success] = "Status canceled successfully";// TODO remove hard code
                return RedirectToAction(nameof(OrderDetalis), new { orderId });
            }

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> GetAll(string status)
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            if (!User.IsInRole(SD.RolaAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.
                                                   FirstOrDefault()?.Value;
            }
            ResponseDto response = await _orderService.GetAllOrders(userId);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
                switch (status)//TODO remove hard code
                {
                    case "approved":
                        list = list.Where(u => u.Status == SD.Status_Approved);
                        break;
                    case "readyforpickup":
                        list = list.Where(u => u.Status == SD.Status_ReadyForPickup);
                        break;
                    case "canceled":
                        list = list.Where(u => u.Status == SD.Status_Canceled || u.Status == SD.Status_Refunded);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list });
        }
    }
}
