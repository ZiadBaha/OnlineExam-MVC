using OnlineExam.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.Entities
{
    public class Exam : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public ExamDifficulty Difficulty { get; set; }

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();
    }
}
