using OnlineExam.Core.DTOs.Program.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Program
{
    public class SubmitExamDto
    {
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public List<int> AnswerIds { get; set; }

        public ExamResultCreateDto ToAddResult()
        {
            // حساب النتيجة هنا
            var correctAnswers = this.AnswerIds.Count(a => a == 1);
            var totalQuestions = this.AnswerIds.Count;
            var score = (int)((correctAnswers / (double)totalQuestions) * 100);

            // إنشاء الـ ExamResultCreateDto
            return new ExamResultCreateDto
            {
                UserId = this.UserId,
                ExamId = this.ExamId,
                CorrectAnswers = correctAnswers,
                TotalQuestions = totalQuestions,
                Score = score,
                IsPassed = score >= 60 
            };
        }
    }
}

