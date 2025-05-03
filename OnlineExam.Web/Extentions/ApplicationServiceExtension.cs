using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Data.DbContext;
using OnlineExam.Web.Helpers;
using AutoMapper;
using OnlineExam.Core.Entities.Common;
using OnlineExam.Application.Services;
using OnlineExam.Infrastructure.Repositories.Auth;
using OnlineExam.Application.Services.Admin;
using OnlineExam.Infrastructure.Repositories.Auth.Admin;
using OnlineExam.Core.DTOs.Program.ExamDto;


namespace OnlineExam.Web.Extentions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Database Context Configuration
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            // AutoMapper Configuration
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            // Add additional services like repositories
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IGenericCrudService<AddExamDto, UpdateExamDto, GetExamDto>, ExamService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IChoiceService, ChoiceService>();
            services.AddScoped<IExamResultService, ExamResultService>();
            services.AddScoped<IQuestionService, QuestionService>();

            return services;
        }

    }
}
