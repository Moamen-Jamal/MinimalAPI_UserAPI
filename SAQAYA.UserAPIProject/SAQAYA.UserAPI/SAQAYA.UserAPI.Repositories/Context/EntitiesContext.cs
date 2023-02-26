using Microsoft.EntityFrameworkCore;
using SAQAYA.UserAPI.Entities.Entities;

namespace SAQAYA.UserAPI.Repositories
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext(DbContextOptions options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

    }

}
