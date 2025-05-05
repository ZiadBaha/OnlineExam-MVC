using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.DTOs.Program.questionDto;
using OnlineExam.Core.Entities.Identity;


namespace OnlineExam.Web.Controllers.User
{
    public class QuizesController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        private readonly UserManager<AppUser> _userManager;
        public QuizesController(IExamService examService, IQuestionService questionService, UserManager<AppUser> userManager)
        {
            _examService = examService;
            _questionService = questionService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _examService.GetAllAsync();
            return View(result.Data);
        }
        public async Task<IActionResult> ExamQuestions(int id)
        {
            var exam = await _examService.GetByIdAsync(id);

            if (!exam.Success)
                return NotFound();

            Console.WriteLine($"Exam: {exam.Data.Title}, Difficulty: {exam.Data.Difficulty}");

            foreach (var question in exam.Data.questionDtos)
            {
                Console.WriteLine($"Question: {question.Title}");

                foreach (var choice in question.Choices)
                {
                    Console.WriteLine($"  - Choice: {choice.Text} (Id: {choice.Id})");
                }
            }

            return View(exam.Data);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitExam(SubmitExamDto dto)
        {
            var result = await _examService.SubmitExamAsync(dto);
            return Ok(result);
        }



    }
}
