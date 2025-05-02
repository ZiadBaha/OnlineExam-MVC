using Microsoft.AspNetCore.Identity;
using OnlineExam.Infrastructure.Data.DbContext;

namespace OnlineExam.Web.Extentions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
