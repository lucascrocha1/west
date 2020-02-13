namespace Identity.API
{
    using Identity.API.Infrastructure;
    using Identity.API.Services.Auth;
    using Identity.API.Services.User;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            services.AddDbContext<IdentityContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseCors();

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