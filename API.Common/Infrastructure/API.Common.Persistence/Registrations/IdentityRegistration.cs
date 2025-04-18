using API.Common.Application.Abstractions.MenuRequirement;
using API.Common.Application.IdentityMessage;
using API.Common.Domain.Roles;
using API.Common.Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Common.Persistence.Registrations
{
    public static class IdentityRegistration
    {
        public static void AddIdentityRegistration<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+üğışçöÜĞİŞÇÖ";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityErrorDescriber>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Menus", policy =>
                policy.Requirements.Add(new MenuAccessRequirement()));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine($"Token successfully validated for User: {context.Principal.Identity.Name}");
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication Failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddControllers();
        }
    }
}
