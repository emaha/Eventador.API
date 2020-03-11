using Eventador.Domain;
using Microsoft.EntityFrameworkCore;

namespace Eventador.Data
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class EventadorDbContext : DbContext
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        public EventadorDbContext(DbContextOptions<EventadorDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Participation> Participations { get; set; }
    }
}
