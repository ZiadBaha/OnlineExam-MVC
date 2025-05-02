using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OnlineExam.Application.Services;
using OnlineExam.Core.DTOs.Identity;
using OnlineExam.Core.Entities.Common;
using OnlineExam.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure.Repositories.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<AuthService> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<MessagesResponse<string>> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new MessagesResponse<string>(401, null, "Invalid Email or Password");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new MessagesResponse<string>(401, null, "Invalid Email or Password");
            }

            _logger.LogInformation($"User {user.Email} logged in.");

            return new MessagesResponse<string>(200, "Logged in successfully");
        }

        public async Task<MessagesResponse<string>> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return new MessagesResponse<string>(200, "Logged out successfully");
        }


     
    }

}
