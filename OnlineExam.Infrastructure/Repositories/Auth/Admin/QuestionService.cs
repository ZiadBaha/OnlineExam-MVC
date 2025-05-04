using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.questionDto;
using OnlineExam.Core.Entities;
using OnlineExam.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class QuestionService : GenericCrudService<Question, QuestionCreateDto, QuestionUpdateDto, QuestionDto>, IQuestionService
    {
        public QuestionService(IMapper mapper, ApplicationDbContext context) : base(mapper, context) { }
    
        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsOfExam(int examId)
        {
            var questions = await _context.Questions
                .Where(q => q.ExamId == examId)
                .Include(q => q.Choices) 
                .ToListAsync();

            var questionDtos = _mapper.Map<IEnumerable<QuestionDto>>(questions);

            return questionDtos;
        }
    }
}