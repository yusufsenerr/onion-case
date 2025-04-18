using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
