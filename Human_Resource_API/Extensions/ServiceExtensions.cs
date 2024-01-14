using Human_Resource_API.Data;
using Human_Resource_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Human_Resource_API.Extensions
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
            option.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.WithHeaders("accept", "content-type");
                builder.WithMethods("POST", "GET", "PUT", "DELETE", "PATCH");

                });
            });
        }


        public static void ConfigureIdentity(this IServiceCollection services) 
        { var builder = services.AddIdentity<User, IdentityRole>(o =>
        { o.Password.RequireDigit = true;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 10;
            o.User.RequireUniqueEmail = true; }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); }

    }
}
