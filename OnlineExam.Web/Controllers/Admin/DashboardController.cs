using Microsoft.AspNetCore.Mvc;

namespace OnlineExam.Web.Controllers.Admin
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
