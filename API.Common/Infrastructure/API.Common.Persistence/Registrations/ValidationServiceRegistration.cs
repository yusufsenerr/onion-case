using API.Common.Application.Validators.Balance;
using API.Common.Application.Validators.Order;
using API.Common.Application.Validators.Product;
using API.Common.Application.Validators.Role;
using API.Common.Application.Validators.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Common.Persistence.Registrations
{
    public static class ValidationServiceRegistration
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<OrderItemDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<OrderItemValidator>();

            services.AddValidatorsFromAssemblyContaining<BalanceValidator>();

            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            services.AddValidatorsFromAssemblyContaining<AppRoleValidator>();

            services.AddValidatorsFromAssemblyContaining<AppUserValidator>();

            return services;
        }
    }


}
