using System;
using System.Runtime.CompilerServices;
using AutoMappingExercise.Data;
using AutoMappingExercise.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AutoMappingExercise.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly AutoMappingExerciseContext context;

        public DbInitializerService(AutoMappingExerciseContext context)
        {
            this.context = context;
        }

        public void IntializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}