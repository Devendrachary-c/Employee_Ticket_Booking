using Employee_Ticket_Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Ticket_Booking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<EmployeeTravel> EmployeeTravels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<TravelRequest> TravelRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: configuring a relationship or adding constraints
            modelBuilder.Entity<EmployeeTravel>().ToTable("EmployeeTravels");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<TravelRequest>().ToTable("TravelRequests");

            //modelBuilder.Entity<UserType>().HasData(
            //    new UserType { Id = 1, Name = "Admin" },
            //    new UserType { Id = 2, Name = "Employee" },
            //    new UserType { Id = 3, Name = "Manager" }
            // );



        }
    }
}
