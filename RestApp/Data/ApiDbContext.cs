using Microsoft.EntityFrameworkCore;
using RestApp.Models;

namespace RestApp.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<Region> Items {get;set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
         //   Database.EnsureCreated();
        }
    }
}