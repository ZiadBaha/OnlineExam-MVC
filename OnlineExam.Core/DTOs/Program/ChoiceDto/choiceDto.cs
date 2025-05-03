﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Program.ChoiceDto
{
    public class choiceDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }

}
