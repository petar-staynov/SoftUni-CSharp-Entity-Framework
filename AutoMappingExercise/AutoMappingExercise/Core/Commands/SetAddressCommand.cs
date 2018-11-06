using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Core.Contracts;

namespace AutoMappingExercise.Core.Commands
{
    class SetAddressCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public SetAddressCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            string address = args[1];

            employeeController.SetAddress(id, address);
            return "Address chanegd succesfully!";

        }
    }
}