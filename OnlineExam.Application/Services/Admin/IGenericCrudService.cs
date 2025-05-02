using OnlineExam.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IGenericCrudService<TAddDto, TUpdateDto, TGetDto>
    {
        Task<MessagesResponse<string>> AddAsync(TAddDto dto);
        Task<MessagesResponse<string>> UpdateAsync(TUpdateDto dto);
        Task<MessagesResponse<string>> DeleteAsync(int id);
        Task<MessagesResponse<TGetDto>> GetByIdAsync(int id);
        Task<MessagesResponse<List<TGetDto>>> GetAllAsync();
    }

}
