using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using timeTracker.Models;

namespace timeTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    Name = "Jessie's Birthday Party",
                    Description = "Throwing a surprise Birthday Party for Jessie",
                    start = new DateTime(2021, 1, 15, 5, 0, 0),
                    end = new DateTime(2021, 1, 15, 8, 0, 0)
                });
        }

    }
}
