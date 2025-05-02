using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ExamDto;
using OnlineExam.Core.Enums;

namespace OnlineExam.Web.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ExamController : Controller
    {
        private readonly IGenericCrudService<AddExamDto, UpdateExamDto, GetExamDto> _examService;

        public ExamController(IGenericCrudService<AddExamDto, UpdateExamDto, GetExamDto> examService)
        {
            _examService = examService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _examService.GetAllAsync();
            return View(result.Data); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddExamDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _examService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _examService.GetByIdAsync(id);
            if (!result.Success) return NotFound();

            var updateDto = new UpdateExamDto
            {
                Id = result.Data.Id,
                Title = result.Data.Title,
                Difficulty = result.Data.Difficulty
            };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateExamDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _examService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _examService.GetByIdAsync(id);
            if (!result.Success) return NotFound();

            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _examService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _examService.GetByIdAsync(id);
            if (!result.Success) return NotFound();

            return View(result.Data);
        }
    }
}
