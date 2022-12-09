using CabManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CabManagement.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) { }

        public DbSet<User>Users { get; set; }
        

        public DbSet<CabDetail> CabDetails { get; set; }
        public DbSet<CabSchedule> CabSchedules { get; set; }
        public DbSet<Booking> Bookings { get; set; }

       



    }
}
