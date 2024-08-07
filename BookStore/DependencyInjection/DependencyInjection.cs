﻿using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Repository.Models;
using Repository.UnitOfwork;

namespace BookStore.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<BStoreDBContext>(options => options.UseSqlServer(getConnection()));
            return services;
        }
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();
            return services;
        }
        public static IServiceCollection addUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfwork, UnitOfwork>();
            return services;
        }



        public static string getConnection()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var str = config["ConnectionStrings:MyDB"];
            return str;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services,IConfiguration configuration)
        {
            PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));
            
            services.AddSingleton(payOS);

            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });
            return services;
        }

    }


}
