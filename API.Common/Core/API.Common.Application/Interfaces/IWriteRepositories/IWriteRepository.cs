using API.Common.Domain.Commons;
using API.Common.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Interfaces.IWriteRepositories
{
    public interface IWriteRepository<TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
    {
        Task<BaseResponse> CreateOrUpdateAsync(TEntity model);
        Task<BaseResponse> BulkCreateOrUpdateAsync(List<TEntity> models);
        Task<TEntity> AddAsync(TEntity model);
        Task<bool> AddRangeAsync(List<TEntity> datas);
        bool Remove(TEntity model);
        bool RemoveRange(List<TEntity> datas);
        Task<BaseResponse> RemoveAsync(string id);
        bool Update(TEntity model);
        Task<int> SaveAsync();
        Task<BaseResponse> RemoveRangeAsync(List<string> ids);
    }
}