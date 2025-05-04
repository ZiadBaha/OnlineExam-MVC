using OnlineExam.Core.DTOs.Program.questionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IQuestionService : IGenericCrudService<QuestionCreateDto, QuestionUpdateDto, QuestionDto> 
    {

        Task<List<QuestionDto>> GetQuestionsByExamIdAsync(int examId);
    }

}
