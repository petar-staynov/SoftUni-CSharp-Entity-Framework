using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Core.Contracts;
using AutoMappingExercise.Core.Dto;
using AutoMappingExercise.Core.Dtos;

namespace AutoMappingExercise.Core.Commands
{
    class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public EmployeePersonalInfoCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            EmployeeFullDto employeeDto = employeeController.GetFullEmployeeInfo(id);

            StringBuilder sb = new StringBuilder();
            sb.a

        }
    }
}