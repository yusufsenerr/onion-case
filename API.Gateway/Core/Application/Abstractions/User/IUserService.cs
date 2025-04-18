using Application.DTOs.Authentication;
using Domain;

namespace Application.Abstractions.User
{
    public interface IUserService
    {
        Task<BaseResponse> CreateUser(CreateUser model);
    }
}
