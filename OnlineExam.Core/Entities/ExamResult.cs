using OnlineExam.Core.Entities.Identity;

namespace OnlineExam.Core.Entities
{
    public class ExamResult : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}