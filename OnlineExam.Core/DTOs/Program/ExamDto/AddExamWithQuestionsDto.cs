using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.DTOs.Program.questionDto;

namespace OnlineExam.Core.DTOs.Program.ExamDto
{
    public class AddExamWithQuestionsDto
    {
        public AddExamDto Exam { get; set; }

        public List<QuestionCreateDto> Questions { get; set; } = new List<QuestionCreateDto>();
    }
  

}
