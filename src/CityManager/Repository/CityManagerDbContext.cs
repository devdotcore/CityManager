using Microsoft.EntityFrameworkCore;

namespace CityManager.Repository
{
    public class CityManagerDbContext : DbContext
    {
        public CityManagerDbContext(DbContextOptions<CityManagerDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(i =>
            {
                i.HasKey(m => m.CityId);
            });

            modelBuilder.Entity<Country>(i =>
            {
                i.HasKey(m => m.CountryId);
            });
        }
    }
}