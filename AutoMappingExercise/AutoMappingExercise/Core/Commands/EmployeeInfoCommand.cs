using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Core.Contracts;
using AutoMappingExercise.Core.Dto;

namespace AutoMappingExercise.Core.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeController employeeController;
        public EmployeeInfoCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            EmployeeDto employeeDto = employeeController.GetEmployeeInfo(id);

            return $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary}";
        }
    }
}