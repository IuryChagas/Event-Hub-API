using Event_Hub_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Hub_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
     {

     }   
    }
}