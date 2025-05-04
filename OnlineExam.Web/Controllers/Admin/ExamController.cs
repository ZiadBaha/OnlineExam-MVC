using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.DTOs.Program.ExamDto;
using OnlineExam.Core.DTOs.Program.questionDto;
using OnlineExam.Core.Enums;
using OnlineExam.Infrastructure.Repositories.Auth.Admin;

namespace OnlineExam.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ExamController : Controller
    {
        private readonly IExamService _service;
        private readonly IQuestionService _questionService;
        private  readonly IChoiceService _choiceService;

        public ExamController(IExamService service, IQuestionService questionService , IChoiceService choiceService)
        {
            _service = service;
            _questionService = questionService;
            _choiceService = choiceService;

        }

        public async Task<IActionResult> Index()
        {
            var exams = await _service.GetAllAsync();
            return View(exams.Data);
        }

        // في الـ GET
        public IActionResult Create()
        {
            var model = new AddExamWithQuestionsDto
            {
                Questions = new List<QuestionCreateDto>() // تأكد هنا إنك بتستخدم نوع الـ QuestionCreateDto
            };

            return View(model); // Return the initialized model to the view
        }

        // في الـ POST
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(AddExamWithQuestionsDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //add exam
            var examResponse = await _service.AddAsync(model.Exam);
            var examIdString = examResponse.Data;

            if (!int.TryParse(examIdString, out int examId))
            {
                ModelState.AddModelError(string.Empty, "Invalid exam ID.");
                return View(model);
            }

            //add questions and choices
            foreach (var question in model.Questions)
            {
               
                var questionDto = new QuestionCreateDto
                {
                    Title = question.Title,
                    ExamId = examId,  
                };
                 
                var questionResponse = await _questionService.AddAsync(questionDto);
                var questionIdString = questionResponse.Data;  
                if (!int.TryParse(questionIdString, out int questionId))
                {
                    ModelState.AddModelError(string.Empty, "Invalid exam ID.");
                    return View(model);
                }
                 
                foreach (var choice in question.Choices)
                {
                    var choiceDto = new ChoiceCreateDto
                    {
                        Text = choice.Text,
                        IsCorrect = choice.IsCorrect,
                        QuestionId =questionId   
                    };
                          
                    await _choiceService.AddAsync(choiceDto);
                }
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _service.GetByIdAsync(id);
            if (!exam.Success) return NotFound();

            var dto = new UpdateExamDto
            {
                Id = exam.Data.Id,
                Title = exam.Data.Title,
                Difficulty = (ExamDifficulty)Enum.Parse(typeof(ExamDifficulty), exam.Data.Difficulty.ToString())
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateExamDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _service.GetByIdAsync(id);
            return View(exam.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
