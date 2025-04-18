using API.Common.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Interfaces.IRepositoies
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
