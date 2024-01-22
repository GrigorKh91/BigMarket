using BigMarket.Web.Models;
using BigMarket.Web.Models.AuthApi;
using BigMarket.Web.Service.IService;
using BigMarket.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigMarket.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem { Text=SD.RolaAdmin, Value= SD.RolaAdmin },
                new SelectListItem { Text=SD.RolaCastomer, Value= SD.RolaCastomer },
            };
            ViewBag.RoleList = roleList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;
            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RolaCastomer;
                }
                assignRole = await _authService.AssignRoleAsync(obj);
                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successful"; // TODO change from hard code
                    return RedirectToAction(nameof(Login));
                }
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem { Text=SD.RolaAdmin, Value= SD.RolaAdmin },
                new SelectListItem { Text=SD.RolaCastomer, Value= SD.RolaCastomer },
            };
            ViewBag.RoleList = roleList;
            TempData["error"] = result?.Message;
            return View(obj);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
