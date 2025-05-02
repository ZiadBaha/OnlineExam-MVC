using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.Entities.Common;
using OnlineExam.Core.Entities.Identity;
using OnlineExam.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth.Admin
{
    public class AdminUserService : IAdminUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminUserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<MessagesResponse<string>> AddUserAsync(AddUserDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return new MessagesResponse<string>(409, null, "Email already exists.");
            }

            var user = _mapper.Map<AppUser>(model);
            user.Role = UserRoles.User; 

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new MessagesResponse<string>(400, null, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return new MessagesResponse<string>(201, "User created successfully.");
        }

        public async Task<MessagesResponse<string>> UpdateUserAsync(UpdateUserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return new MessagesResponse<string>(404, null, "User not found.");

            _mapper.Map(model, user);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new MessagesResponse<string>(400, null, string.Join(", ", result.Errors.Select(e => e.Description)));

            return new MessagesResponse<string>(200, "User updated successfully.");
        }

    }

}
