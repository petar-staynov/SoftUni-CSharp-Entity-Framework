using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Core.Contracts;
using AutoMappingExercise.Core.Dto;

namespace AutoMappingExercise.Core.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeController employeeController;
        public AddEmployeeCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }


        public string Execute(string[] args)
        {
            string firstname = args[0];
            string lastname = args[1];
            decimal salary = decimal.Parse(args[2]);

            EmployeeDto employeeDto = new EmployeeDto
            {
                FirstName = firstname,
                LastName = lastname,
                Salary = salary
            };

            this.employeeController.AddEmployee(employeeDto);
            return $"Employee {employeeDto.FirstName} {employeeDto.LastName} added succesfully";
        }
    }
}
