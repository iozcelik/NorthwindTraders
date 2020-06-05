using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Application.Common.Interfaces;
using Northwind.Common;
using Northwind.Infrastructure.Files;
using Northwind.Infrastructure.Identity;

namespace Northwind.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddScoped<IUserManager, UserManagerService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NorthwindDatabase")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddAuthentication();

            return services;
        }
    }
}
