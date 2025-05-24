namespace CarRentalApi.DbContext
{
    using CarRentalApi.Models;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingType> BookingTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Contact>().OwnsMany(c => c.Addresses, a =>
            {
                a.WithOwner().HasForeignKey("ContactId"); 
                a.Property<int>("Id");                    
                a.HasKey("Id");                           
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
