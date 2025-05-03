using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Program.ChoiceDto;
using OnlineExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class ChoiceService : GenericCrudService<Choice, ChoiceCreateDto, ChoiceUpdateDto, choiceDto>, IChoiceService
    {
        public ChoiceService(IMapper mapper, DbContext context) : base(mapper, context) { }
    }

}
