using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.questionDto;

namespace OnlineExam.Web.Controllers.Admin
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _service;

        public QuestionController(IQuestionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var questions = await _service.GetAllAsync();
            return View(questions.Data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(QuestionCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var question = await _service.GetByIdAsync(id);
            if (!question.Success) return NotFound();

            var dto = new QuestionUpdateDto
            {
                Id = question.Data.Id,
                Title = question.Data.Title,
                ExamId = question.Data.ExamId
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(QuestionUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var question = await _service.GetByIdAsync(id);
            return View(question.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
