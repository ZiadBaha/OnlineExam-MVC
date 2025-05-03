using OnlineExam.Core.DTOs.Program.ChoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Program.questionDto
{
    public class QuestionUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public List<ChoiceUpdateDto> Choices { get; set; } = new();
    }

}
