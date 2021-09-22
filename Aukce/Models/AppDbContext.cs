using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aukce.Models
{
    class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext() : base()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Aukce.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Item>()
                .HasOne(ot => ot.Creator)
                    .WithMany(pl => pl.Auctions)
                    .HasForeignKey(ot => new { ot.UserId })
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Name = "admin", Password = "admin" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, Name = "user", Password = "user" });

            modelBuilder.Entity<Item>().HasData(new Item { Id = 1, UserId = 1, Name = "Soon", Description="Prodavam fakt levne ponožky" ,End = new DateTime(2022, 2, 4, 14, 56, 0),  Price = 30 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 2, UserId = 1, Name = "Late", End = new DateTime(2020, 2, 4, 14, 56, 0), Price = 69 });
        }
    }
}
