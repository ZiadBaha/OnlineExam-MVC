namespace OnlineExam.Core.Entities
{
    public class Question : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public ICollection<Choice> Choices { get; set; } = new HashSet<Choice>();
    }
}