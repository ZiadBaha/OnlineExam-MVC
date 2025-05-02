using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IAdminUserService
    {
        Task<MessagesResponse<string>> AddUserAsync(AddUserDto model);
        Task<MessagesResponse<string>> UpdateUserAsync(UpdateUserDto model);

    }
}
