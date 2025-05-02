using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services
{

    public interface IAuthService
    {
        Task<MessagesResponse<string>> LoginAsync(LoginDto model);
        Task<MessagesResponse<string>> LogoutAsync();

    }

}


