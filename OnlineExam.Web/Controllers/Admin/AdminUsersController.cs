using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Identity;

namespace OnlineExam.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUsersController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _adminUserService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _adminUserService.GetUserByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Add()
        {
            return View(new AddUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _adminUserService.AddUserAsync(model);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(string id)
        {
            var user = await _adminUserService.GetUserByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _adminUserService.UpdateUserAsync(model);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(model);
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _adminUserService.DeleteUserAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] = result.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
