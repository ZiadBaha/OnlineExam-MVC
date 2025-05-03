using OnlineExam.Core.DTOs.Program.ChoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IChoiceService : IGenericCrudService<ChoiceCreateDto, ChoiceUpdateDto, choiceDto>
    { 

    }

}
