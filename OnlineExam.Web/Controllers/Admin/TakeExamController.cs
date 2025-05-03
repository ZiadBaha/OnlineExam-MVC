using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program;

namespace OnlineExam.Web.Controllers.Admin
{
    public class TakeExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        private readonly IChoiceService _choiceService;
        private readonly IExamResultService _examResultService;

        public TakeExamController(
            IExamService examService,
            IQuestionService questionService,
            IChoiceService choiceService,
            IExamResultService examResultService)
        {
            _examService = examService;
            _questionService = questionService;
            _choiceService = choiceService;
            _examResultService = examResultService;
        }

        public async Task<IActionResult> Start(int examId)
        {
            var exam = await _examService.GetByIdAsync(examId);
            if (!exam.Success) return NotFound();
            return View(exam.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitExam(SubmitExamDto dto)
        {
        
            await _examResultService.AddAsync(dto.ToAddResult());
            return RedirectToAction("Index", "Home");
        }
    }
}
