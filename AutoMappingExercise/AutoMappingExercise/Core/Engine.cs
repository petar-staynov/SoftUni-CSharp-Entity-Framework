using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Core.Contracts;
using AutoMappingExercise.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMappingExercise.Core
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var initializeDb = serviceProvider.GetService<IDbInitializerService>();
            initializeDb.IntializeDatabase();

            var commandInterpreter = serviceProvider.GetService<ICommandInterpreter>();
            while (true)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string result = commandInterpreter.Read(input);
                Console.WriteLine(result);
            }
        }
    }
}