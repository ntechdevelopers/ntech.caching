using Microsoft.EntityFrameworkCore;
using Ntech.Caching.Models;

namespace Ntech.Caching.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                p.HasNoKey();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
