using BusToursInEurope.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<City> Cities => Set<City>();
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<RouteBus> Routes => Set<RouteBus>();
        public DbSet<Tour> Tours => Set<Tour>();
        public DbSet<Bus> Buses => Set<Bus>();
        public DbSet<User> Users => Set<User>();
        public DbSet<WayPoint> WayPoints => Set<WayPoint>();

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dirMyDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dbPath = Path.Combine(dirMyDocs, "BusTourData", "BusTourInEurope.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
