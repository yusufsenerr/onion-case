using API.Common.Domain.Logs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class WatchDogDbContext : DbContext
    {
        public WatchDogDbContext(DbContextOptions<WatchDogDbContext> options) : base(options)
        {
        }
        public DbSet<WatchDogLogModel> WatchDog_Logs { get; set; }
    }
}