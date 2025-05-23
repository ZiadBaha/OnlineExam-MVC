﻿using OnlineExam.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.DTOs.Identity
{
    public class UpdateUserDto
    {
        [Required]
        public string Id { get; set; }  

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public UserRoles Role { get; set; } = UserRoles.User;
    }

}
