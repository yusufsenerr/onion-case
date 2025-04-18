using API.Common.Application.Abstractions.MenuRequirement;
using API.Common.Application.DTOs.Queries.Autentication;
using API.Common.Application.DTOs.Queries.Balance;
using API.Common.Application.Features.Commands.Authentications.Create.Login;
using API.Common.Application.Features.Commands.Authentications.Create.Register;
using API.Common.Application.Features.Commands.Authentications.Remove;
using API.Common.Application.Features.Commands.Authentications.Update;
using API.Common.Application.Features.Commands.Role.Create;
using API.Common.Application.Features.Commands.Role.Remove;
using API.Common.Application.Features.Queries.Authentications;
using API.Common.Application.Features.Queries.Balance.GetBalance;
using API.Common.Application.Features.Queries.Role;
using API.Common.Application.Features.Queries.Role.RolePermission;
using API.Common.Application.JwtTokens;
using API.Common.Domain.Balance;
using API.Common.Domain.Commons;
using API.Common.Domain.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System.Reflection;

namespace Persistence.Mapping
{
    public static class MediaTrAndDependencyInjection
    {
        public static void MediaTrMappingAddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IAuthorizationHandler, MenuAccessHandler>();

            #region User
            services.AddScoped<IRequestHandler<GetAllUserQueryRequest, List<UserDto>>, GetAllUserQueryHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<GetUserByIdQueryRequest, UserDto>, GetUserByIdQueryHandler<ApplicationDbContext>>();
            services.AddScoped<GenerateJwtToken>();
            services.AddScoped<IRequestHandler<LoginUserCommandRequest, BaseResponse>, LoginUserCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<RegisterUserCommandRequest, BaseResponse>, RegisterUserCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<UpdateUserCommandRequest, BaseResponse>, UpdateUserCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<RemoveUserCommandRequest, BaseResponse>, RemoveUserCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<ResetPasswordCommandRequest, BaseResponse>, ResetPasswordCommandHandler<ApplicationDbContext>>();


            #endregion

            #region Balance
            services.AddScoped<IRequestHandler<GetBalanceByIdQueryRequest, UserBalanceDto>, GetBalanceByIdQueryHandler<ApplicationDbContext>>();
            #endregion

            #region Role
            services.AddScoped<IRequestHandler<CreateRolePermissionsCommandRequest, BaseResponse>, CreateRolePermissionsCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<RegisterRoleCommandRequest, BaseResponse>, RegisterRoleCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<RemoveRoleCommandRequest, BaseResponse>, RemoveRoleCommandHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<GetAllRoleQueryRequest, List<AppRole>>, GetAllRoleQueryHandler<ApplicationDbContext>>();
            services.AddScoped<IRequestHandler<GetRolePermissionsQueryRequest, List<RoleMenuPermissions>>, GetRolePermissionsQueryHandler<ApplicationDbContext>>();

            #endregion
        }
    }
}
