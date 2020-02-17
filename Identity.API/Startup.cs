namespace Identity.API
{
    using Identity.API.Infrastructure.Contexts;
    using Identity.API.Infrastructure.Services.Authentication;
    using Identity.API.Infrastructure.Services.Email;
    using Identity.API.Infrastructure.Services.User;
    using Infrastructure.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Reflection;

    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddControllersWithViews()
                .AddNewtonsoftJson();

            services.AddTransient<ILoginService, LoginService>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<IdentityContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("LoginConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            });

            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Identity API",
                    Description = "Identity Service API"
                });
            });

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(policy =>
                {
                    var corsOrigins = Configuration.GetSection("CorsAllowedOrigins").Get<string[]>();

                    if (corsOrigins != null)
                        policy.WithOrigins(corsOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            services.AddAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors();

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(opts =>
            {
                opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
                opts.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}