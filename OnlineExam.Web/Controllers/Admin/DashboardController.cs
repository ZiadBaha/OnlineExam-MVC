using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;

namespace OnlineExam.Web.Controllers.Admin
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var totalUsers = await _dashboardService.GetUserCountAsync();
            var totalQuizzes = await _dashboardService.GetExamCountAsync();

            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalQuizzes = totalQuizzes;

            return View();
        }
    }
}
