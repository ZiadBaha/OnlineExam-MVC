using OnlineExam.Core.DTOs.Program.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IExamResultService : IGenericCrudService<ExamResultCreateDto, UpdateExamResultDto, ExamResultDto> 
    { 

    }

}
