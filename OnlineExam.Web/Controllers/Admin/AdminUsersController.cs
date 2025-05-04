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

        [HttpGet]
        public IActionResult AddUser()
        {
            return View(new AddUserDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please check the input fields.";
                return View(model);
            }

            var response = await _adminUserService.AddUserAsync(model);

            if (response.StatusCode == 201) // Success
            {
                ViewBag.Message = "User created successfully!";
                return RedirectToAction("AddUser");
            }
            else
            {
                ViewBag.Message = response.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult UpdateUser(string id)
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _adminUserService.UpdateUserAsync(model);

            if (!result.Success)
            {
                ViewBag.Message = result.Message;
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
