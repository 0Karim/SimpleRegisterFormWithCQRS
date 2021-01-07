
using CleanArch.Application.Common.Interfaces;
using CleanArch.Infrastructure.Persistence;
using CleanArch.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(item => item.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
