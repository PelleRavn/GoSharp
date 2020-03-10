using System;
using System.Linq;
using GoSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoSharp.DbContexts
{
    public class GoContext : DbContext
    {
        public GoContext()
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=go.db");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Link>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }
    }
}
