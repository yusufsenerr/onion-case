using Microsoft.Extensions.Configuration;
using Persistence.Context;

namespace EKSystemApp.Persistence.DbInitiliazers
{
    public interface IDbInitiliazerContext
    {
        Task Initialize(ApplicationDbContext context, IConfiguration configurations);
    }
}
