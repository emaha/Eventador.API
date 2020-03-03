using Eventador.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventador.Data
{
    public class EventadorDbContext : DbContext
    {
        public EventadorDbContext(DbContextOptions<EventadorDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Mark> Marks { get; set; }
    }
}
