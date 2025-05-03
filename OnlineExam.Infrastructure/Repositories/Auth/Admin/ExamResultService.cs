using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ResultDto;
using OnlineExam.Core.Entities;
using OnlineExam.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class ExamResultService : GenericCrudService<ExamResult, ExamResultCreateDto, UpdateExamResultDto, ExamResultDto>, IExamResultService
    {
        public ExamResultService(IMapper mapper, DbContext context) : base(mapper, context) 
        { 

        
        }
        public override async Task<MessagesResponse<string>> UpdateAsync(UpdateExamResultDto dto)
        {
            return new MessagesResponse<string>(400, null, "Updating exam results is not allowed");
        }

    }

}
