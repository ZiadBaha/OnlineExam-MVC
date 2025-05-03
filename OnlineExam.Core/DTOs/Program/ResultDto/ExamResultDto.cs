using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Program.ResultDto
{
    public class ExamResultDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public int ExamId { get; set; }
        public string ExamTitle { get; set; } = string.Empty;

        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }
        public DateTime SubmittedAt { get; set; }
    }

}
