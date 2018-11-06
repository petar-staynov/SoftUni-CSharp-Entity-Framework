using System;
using AutoMappingExercise.Core.Dto;
using AutoMappingExercise.Core.Dtos;

namespace AutoMappingExercise.Core.Contracts
{
    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDto employeeDto);
        void SetBirthday(int employeeId, DateTime date);
        void SetAddress(int employeeId, string address);
        EmployeeDto GetEmployeeInfo(int employeeId);
        EmployeeFullDto GetFullEmployeeInfo(int employeeId);
    }
}