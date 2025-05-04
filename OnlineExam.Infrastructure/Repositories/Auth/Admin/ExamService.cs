using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ExamDto;
using OnlineExam.Core.Entities;
using OnlineExam.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class ExamService : GenericCrudService<Exam, AddExamDto, UpdateExamDto, GetExamDto>, IExamService
    {
        public ExamService(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
        {
        }

    }


}
