using Eventador.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventador.API.Data
{
    public class EventadorDbContext : DbContext
    {
        public EventadorDbContext(DbContextOptions<EventadorDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
