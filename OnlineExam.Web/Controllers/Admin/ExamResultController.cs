using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;

namespace OnlineExam.Web.Controllers.Admin
{
    public class ExamResultController : Controller
    {
        private readonly IExamResultService _service;

        public ExamResultController(IExamResultService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _service.GetAllAsync();
            return View(results.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return View(result.Data);
        }
    }

}
