using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Identity
{
    public class UpdateUserRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string NewRole { get; set; }
    }

}
