using System;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    // I am changing DbContext to IdentityDbContext after introduction of Identity framework to this project.
    public class DutchTreatContext: IdentityDbContext<StoreUser>
    {
        public DutchTreatContext(DbContextOptions<DutchTreatContext> options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasData(new Order()
            {
                Id = 1,
                OrderDate = DateTime.Now,
                OrderNumber = "11111"
            });
        }
    }
}