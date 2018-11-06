using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoMappingExercise.Data
{
    public class AutoMappingExerciseContext : DbContext
    {
        public AutoMappingExerciseContext() { }

        public AutoMappingExerciseContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
    }
}
