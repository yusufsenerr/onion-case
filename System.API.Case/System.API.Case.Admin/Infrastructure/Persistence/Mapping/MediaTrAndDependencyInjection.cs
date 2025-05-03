using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Persistence.Mapping
{
    public static class MediaTrAndDependencyInjection
    {
        public static void MediaTrMappingAddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            //#region User
            //services.AddScoped<IRequestHandler<GetAllUserQueryRequest, List<UserDto>>, GetAllUserQueryHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<GetUserByIdQueryRequest, UserDto>, GetUserByIdQueryHandler<ApplicationDbContext>>();
            //services.AddScoped<GenerateJwtToken>();
            //services.AddScoped<IRequestHandler<LoginUserCommandRequest, BaseResponse>, LoginUserCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<RegisterUserCommandRequest, BaseResponse>, RegisterUserCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<UpdateUserCommandRequest, BaseResponse>, UpdateUserCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<RemoveUserCommandRequest, BaseResponse>, RemoveUserCommandHandler<ApplicationDbContext>>();
            //#endregion

            //#region Balance
            //services.AddScoped<IRequestHandler<GetBalanceByIdQueryRequest, UserBalanceDto>, GetBalanceByIdQueryHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<AddUserBalanceCommandRequest, BaseResponse>, AddUserBalanceCommandHandler<ApplicationDbContext>>();
            //#endregion
            //#region Product
            //services.AddScoped<IRequestHandler<CreateProductCommandRequest, BaseResponse>, CreateProductCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<RemoveProductCommandRequest, BaseResponse>, RemoveProductCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<GetProductQueryRequest, List<ProductDto>>, GetProductQueryHandler<ApplicationDbContext>>();
            //#endregion

            //#region Order
            //services.AddScoped<IRequestHandler<CreateOrderCommandRequest, BaseResponse>, CreateOrderCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<GetAllOrderByIdCommandRequest, List<OrderDto>>, GetAllOrderByIdCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<UpdateOrdersToCompletedCommand, Unit>, UpdateOrdersToCompletedCommandHandler<ApplicationDbContext>>();

            //#endregion

            //#region SystemAdmin
            //services.AddScoped<IRequestHandler<CreateSystemAdminCommandRequest, BaseResponse>, CreateSystemAdminCommandHandler<ApplicationDbContext>>();

            //#endregion
            //#region Role
            //services.AddScoped<IRequestHandler<RegisterRoleCommandRequest, BaseResponse>, RegisterRoleCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<RemoveRoleCommandRequest, BaseResponse>, RemoveRoleCommandHandler<ApplicationDbContext>>();
            //services.AddScoped<IRequestHandler<GetAllRoleQueryRequest, List<AppRole>>, GetAllRoleQueryHandler<ApplicationDbContext>>();

            //#endregion
        }
    }

}
