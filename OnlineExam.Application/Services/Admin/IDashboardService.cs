using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Services.Admin
{
    public interface IDashboardService
    {
        Task<int> GetUserCountAsync();
        Task<int> GetExamCountAsync();
    }
}
