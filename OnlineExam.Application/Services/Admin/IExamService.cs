using OnlineExam.Core.DTOs.Program.ExamDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IExamService : IGenericCrudService<AddExamDto, UpdateExamDto, GetExamDto>
    {

    }

}
