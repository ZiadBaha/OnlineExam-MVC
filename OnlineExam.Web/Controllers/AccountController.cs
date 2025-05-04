using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services;
using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.Entities.Identity;

namespace OnlineExam.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(model);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user!);

            if (roles.Contains("Admin"))
                return RedirectToAction("Dashboard", "Admin");
            else
                return RedirectToAction("Index", "Quizes");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
