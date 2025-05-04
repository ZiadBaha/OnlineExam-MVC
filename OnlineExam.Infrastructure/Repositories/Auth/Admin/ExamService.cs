using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.DTOs.Program.ExamDto;
using OnlineExam.Core.DTOs.Program.questionDto;
using OnlineExam.Core.DTOs.Program.ResultDto;
using OnlineExam.Core.Entities;
using OnlineExam.Core.Entities.Common;
using OnlineExam.Core.Entities.Identity;
using OnlineExam.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class ExamService : GenericCrudService<Exam, AddExamDto, UpdateExamDto, GetExamDto>, IExamService
    {
        private readonly UserManager<AppUser> _userManager;
        public ExamService(IMapper mapper, ApplicationDbContext context, UserManager<AppUser> userManager) : base(mapper, context)
        {
            _userManager = userManager;
        }
        public override async Task<MessagesResponse<List<GetExamDto>>> GetAllAsync()
        {
            var exams = await _context.Exams
                .Include(e => e.Questions)
                    .ThenInclude(q => q.Choices)
                .ToListAsync();

            var dtoList = exams.Select(exam => new GetExamDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Difficulty = exam.Difficulty,
                questionDtos = exam.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    ExamId = q.ExamId,
                    Choices = q.Choices.Select(c => new choiceDto
                    {
                        Id = c.Id,
                        Text = c.Text,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                }).ToList()
            }).ToList();

            return new MessagesResponse<List<GetExamDto>>(200, dtoList);
        }

        public override async Task<MessagesResponse<GetExamDto>> GetByIdAsync(int id)
        {
            var exam = await _context.Exams
                .Include(e => e.Questions)
                    .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null)
                return new MessagesResponse<GetExamDto>(404, null, "Exam not found");

            var examDto = new GetExamDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Difficulty = exam.Difficulty,
                questionDtos = exam.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    ExamId = q.ExamId,
                    Choices = q.Choices.Select(c => new choiceDto
                    {
                        Id = c.Id,
                        Text = c.Text,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return new MessagesResponse<GetExamDto>(200, examDto);
        }

        // Implementing SubmitExamAsync as per the structure given
        public async Task<ExamResultDto> SubmitExamAsync(SubmitExamDto dto)
        {
            // Get the correct answer IDs for the exam from the database
            var correctAnswerIds = await _context.Choices
                .Where(c => c.Question.ExamId == dto.ExamId && c.IsCorrect)
                .Select(c => c.Id)
                .ToListAsync();

            // Call ToAddResult to calculate the result from the provided answers
            var resultCreateDto = dto.ToAddResult();

            // Create and save the result in the database
            var examResult = new ExamResult
            {
                UserId = dto.UserId,
                ExamId = dto.ExamId,
                CorrectAnswers = resultCreateDto.CorrectAnswers,
                TotalQuestions = resultCreateDto.TotalQuestions,
                Score = resultCreateDto.Score,
                IsPassed = resultCreateDto.IsPassed,
                SubmittedAt = DateTime.UtcNow
            };

            _context.ExamResults.Add(examResult);
            await _context.SaveChangesAsync();

            // Map the exam result to a DTO
            var examResultDto = new ExamResultDto
            {
                UserId = examResult.UserId,
                ExamId = examResult.ExamId,
                CorrectAnswers = examResult.CorrectAnswers,
                TotalQuestions = examResult.TotalQuestions,
                Score = examResult.Score,
                IsPassed = examResult.IsPassed,
                SubmittedAt = examResult.SubmittedAt
            };

            return examResultDto;
        }













    }


}
