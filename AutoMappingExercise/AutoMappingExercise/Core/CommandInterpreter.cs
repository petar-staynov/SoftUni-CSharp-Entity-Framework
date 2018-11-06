using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMappingExercise.Core.Contracts;

namespace AutoMappingExercise.Core
{
    class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] input)
        {
            string commandName = input[0] + "Command";

            string[] args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                return "Invalid command";
            }

            var constructor = type.GetConstructors().First();
            var constructorParameters = constructor.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var service = constructorParameters.Select(serviceProvider.GetService).ToArray();

            var command = (ICommand)constructor.Invoke(service);

            string result = command.Execute(args);

            return result;
        }
    }
}