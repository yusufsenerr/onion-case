using API.Common.Application.DTOs.Commands.Autentication;
using API.Common.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Abstractions.UserService
{
    public interface IUserService<TContext> where TContext : DbContext
    {
        Task<BaseResponse> UpdateUser(UpdateUserDto model);
        Task<BaseResponse> DeleteUser(Guid id);
    }
}
