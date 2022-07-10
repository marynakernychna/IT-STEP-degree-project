using AutoMapper;
using Core.Helpers;
using Core.Helpers.ApplicationProfiles;
using Core.Interfaces;
using Core.Interfaces.CustomService;
using Core.Services;
using Core.Validation.Authentication;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IIdentityRoleService, IdentityRoleService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITemplateHelper, TemplateHelper>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var configures = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Authentication());
                mc.AddProfile(new UserMap());
            });

            var mapper = configures.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation(
                c => c.RegisterValidatorsFromAssemblyContaining<UserRegistrationValidation>());
        }

        public static void ConfigJwtOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtOptions>(config.GetSection("JwtOptions"));
        }

        public static void Configures(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config);
        }
    }
}
