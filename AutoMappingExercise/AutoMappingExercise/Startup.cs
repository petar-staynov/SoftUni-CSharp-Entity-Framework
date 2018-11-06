using System;
using AutoMapper;
using AutoMappingExercise.Core;
using AutoMappingExercise.Core.Contracts;
using AutoMappingExercise.Core.Controllers;
using AutoMappingExercise.Data;
using AutoMappingExercise.Services;
using AutoMappingExercise.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Profile = AutoMapper.Profile;

namespace AutoMappingExercise
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var service = ConfigureService();
            IEngine engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<AutoMappingExerciseContext>(opts =>
                opts.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();
            serviceCollection.AddTransient<IEmployeeController, EmployeeController>();
            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<AutoMappingExerciseProfile>());

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}