using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ChoiceDto;

namespace OnlineExam.Web.Controllers.Admin
{
    public class ChoiceController : Controller
    {
        private readonly IChoiceService _service;

        public ChoiceController(IChoiceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var choices = await _service.GetAllAsync();
            return View(choices.Data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(ChoiceCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var choice = await _service.GetByIdAsync(id);
            if (!choice.Success) return NotFound();

            var dto = new ChoiceUpdateDto
            {
                Id = choice.Data.Id,
                Text = choice.Data.Text,
                IsCorrect = choice.Data.IsCorrect,
                QuestionId = choice.Data.QuestionId
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChoiceUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var choice = await _service.GetByIdAsync(id);
            return View(choice.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
