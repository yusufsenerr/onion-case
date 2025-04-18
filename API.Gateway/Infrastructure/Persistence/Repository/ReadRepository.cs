using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly APIGatewayDbContext context;
        public ReadRepository(APIGatewayDbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => this.context.Set<T>();

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable();
            return query;
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            var query = Table.Where(method);
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            var query = Table.AsQueryable();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id)
        {
            var query = Table.AsQueryable();
        
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }

    }
}
