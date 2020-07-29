using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using UserServiceAccount.Business.Logics;
using UserServiceAccount.Business.Validators;
using UserServiceAccount.Data.Contexts;
using UserServiceAccount.Data.Repositories;
using UserServiceAccount.Domain.Entities;
using UserServiceAccount.Domain.Interfaces.Business;
using UserServiceAccount.Domain.Interfaces.Infra;

namespace UserServiceAccount.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SqlServerContext>();

            Setup(services);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "User API",
                    Description = "USer API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(string.Format("/swagger/{0}/swagger.json",
                    "v1"),
                    "User API");
            });
        }

        private void Setup(IServiceCollection services)
        {
            #region Validators
            services.AddTransient<IValidator<UserEntity>, UserValidator>();
            #endregion

            #region Repository
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddTransient<IUserLogic, UserLogic>();
            #endregion

            #region Mappers
            services.AddAutoMapper(Assembly.Load("UserServiceAccount.Application"));
            #endregion
        }
    }
}
