using System;
using System.IO;
using System.Linq;
using GoSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoSharp.DbContexts
{
    public class GoContext : DbContext
    {
        private string dbPath;

        public GoContext()
        {
            dbPath = Environment.GetEnvironmentVariable("DATA") ?? "data";

            if (Directory.Exists(dbPath) == false)
            {
                Directory.CreateDirectory(dbPath);
            }

            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Filename={dbPath}/go.db");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Link>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }
    }
}
