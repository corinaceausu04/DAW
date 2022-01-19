using DAW.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data
{
    public class DAWContext : DbContext
    {
        private readonly IConfiguration _config;

        public DAWContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Phones> Phones { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DAWDb"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //one to one
            modelBuilder.Entity<Phones>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Phones> (p => p.UserId);

            //one to many
            modelBuilder.Entity<Order>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.Id_userId);
                
            //many to many
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId });

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                 .HasOne<Product>()
                 .WithMany()
                 .HasForeignKey(o => o.ProductId);

        }

    }
}
