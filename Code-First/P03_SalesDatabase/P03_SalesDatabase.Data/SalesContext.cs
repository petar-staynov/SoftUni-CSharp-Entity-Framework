using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.EntityConfiguration;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContext options)
        {
        }

        //SETS
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        //CONFIGS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new SaleConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());
        }
    }
}