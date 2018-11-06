using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMappingExercise.Core.Contracts;

namespace AutoMappingExercise.Core.Commands
{
    class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public SetBirthdayCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            DateTime newBirthday = DateTime.ParseExact(args[1], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            employeeController.SetBirthday(id, newBirthday); 

            return "Birthday changed successfully!";
        }
    }
}
