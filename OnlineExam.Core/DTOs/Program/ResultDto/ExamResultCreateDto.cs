using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Program.ResultDto
{
    public class ExamResultCreateDto
    {
        public string UserId { get; set; } = string.Empty;
        public int ExamId { get; set; }

        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }
    }

}
