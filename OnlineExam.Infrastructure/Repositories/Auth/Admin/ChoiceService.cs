using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.Entities;
using OnlineExam.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class ChoiceService : GenericCrudService<Choice, ChoiceCreateDto, ChoiceUpdateDto, choiceDto>, IChoiceService
    {
        public ChoiceService(IMapper mapper, ApplicationDbContext context) : base(mapper, context) { }
        public async Task<List<choiceDto>> GetChoicesByQuestionIdAsync(int questionId)
        {
            var choices = await _context.Choices
                .Where(c => c.QuestionId == questionId)
                .ToListAsync();
            return _mapper.Map<List<choiceDto>>(choices);
        }
    }

}
