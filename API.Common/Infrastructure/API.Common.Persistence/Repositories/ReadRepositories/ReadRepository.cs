using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Models;
using API.Common.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using API.Common.Application.Services.LogService;

namespace API.Common.Persistence.Repositories.ReadRepositories
{
    public class ReadRepository<TContext, TEntity>(TContext context,LoggingService loggingService) : IReadRepository<TContext, TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity
    {
        private readonly TContext context = context;
        readonly private LoggingService loggingService = loggingService;

        public DbSet<TEntity> Table => this.context.Set<TEntity>();

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Table.AsQueryable().Where(x=>x.IsDeleted != true);
            this.loggingService.LogAction(typeof(TEntity).Name,
                $"{typeof(TEntity).Name} türündeki varlık çağrıldı!", query);

            if (includes != null)
            {
               
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method)
        {
            var query = Table.Where(method).Where(x => x.IsDeleted != true);
            this.loggingService.LogAction(typeof(TEntity).Name,
                $"{typeof(TEntity).Name} türündeki varlık sorgulama için çağrıldı!", query);

            return query;
        }
        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method)
        {
            var query = Table.AsQueryable().Where(x => x.IsDeleted != true);
            this.loggingService.LogAction(typeof(TEntity).Name,
                $"{typeof(TEntity).Name} tekil sorgulaması için çağrıldı!", query);

            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<TEntity> GetByIdAsync(string id)
        {
            var entityType = this.context.Model.FindEntityType(typeof(TEntity))?.ClrType;

            if (entityType == null)
                throw new Exception("Entity type could not be found.");
            this.loggingService.LogAction(typeof(TEntity).Name,
                $"{typeof(TEntity).Name} tek bir sorgu parametresi için çağrıldı", entityType);

            var query = Table.AsQueryable().Where(x => x.IsDeleted != true);
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

        public IQueryable<TEntity> GetAllPagination(int? pageNumber, int? pageSize, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = Table.AsQueryable().Where(x => x.IsDeleted != true);
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            if(pageSize == 0)
            {
                pageSize = 10;
            }
            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            query = query.OrderBy(e => e.Id); // Burada Id'ye göre sıralama yapılır (Primary Key veya uygun bir alan seçin)

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                return Pagination.ApplyPagination(query, pageNumber, pageSize);
            }
            this.loggingService.LogAction(typeof(TEntity).Name,
                $"{typeof(TEntity).Name} pagination için çağrıldı", query);

            return query;
        }

    }
}