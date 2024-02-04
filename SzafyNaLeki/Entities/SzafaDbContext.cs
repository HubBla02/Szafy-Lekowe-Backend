using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzafyNaLeki.Entities
{
    public class SzafaDbContext : DbContext
    {
        public DbSet<Szafa> Szafy { get; set; }
        public DbSet<Alarm> Alarm { get; set; }
        private string __connectionString = "Server=DESKTOP-E1AFSPB;Database=SzafaDb;TrustServerCertificate=True;Integrated Security=True";
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Szafa>()
                .Property(r => r.Id)
                .IsRequired();

            modelBuilder.Entity<Szafa>()
            .Property(r => r.Temperatura1)
            .IsRequired();

            modelBuilder.Entity<Szafa>()
                .Property(r => r.Temperatura2)
                .IsRequired();

            modelBuilder.Entity<Szafa>()
                .Property(r => r.Alarm)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(__connectionString);
        }
    }
}
