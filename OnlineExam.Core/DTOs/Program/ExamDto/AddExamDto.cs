using OnlineExam.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Program.ExamDto
{
    public class AddExamDto
    {
        public string Title { get; set; }
        public ExamDifficulty Difficulty { get; set; }
    }
}
