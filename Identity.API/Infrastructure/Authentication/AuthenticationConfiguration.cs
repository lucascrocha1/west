namespace Identity.API.Infrastructure.Authentication
{
    using Identity.API.Infrastructure.Contexts;
    using Identity.API.Model;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;

    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(opts =>
                {
                    opts.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("LoginConnection"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                })
                .AddOperationalStore(opts =>
                {
                    opts.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("LoginConnection"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                })
                .Services.AddTransient<IProfileService, ProfileService>();

            return services;
        }
    }
}