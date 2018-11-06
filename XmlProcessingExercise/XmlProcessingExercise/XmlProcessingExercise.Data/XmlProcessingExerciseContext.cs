using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using XmlProcessingExercise.Data.EntityConfig;
using XmlProcessingExercise.Models;

namespace XmlProcessingExercise.Data
{
    public class XmlProcessingExerciseContext : DbContext
    {
        public XmlProcessingExerciseContext(DbContextOptions options) : base(options)
        {
        }

        public XmlProcessingExerciseContext()
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            //modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CategoryProductConfig());

        }
    }
}
