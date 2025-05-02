using Microsoft.AspNetCore.Identity;
using OnlineExam.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Code { get; set; }
        public UserRoles Role { get; set; }

        public ICollection<ExamResult> ExamResults { get; set; } = new HashSet<ExamResult>();
    }
}
