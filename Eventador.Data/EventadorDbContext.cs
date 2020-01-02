using Eventador.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Eventador.Data
{
    public class EventadorDbContext : DbContext
    {
        public EventadorDbContext(DbContextOptions<EventadorDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
