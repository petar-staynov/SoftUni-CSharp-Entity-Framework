using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMappingExercise.Core.Contracts;
using AutoMappingExercise.Core.Dto;
using AutoMappingExercise.Core.Dtos;
using AutoMappingExercise.Data;
using AutoMappingExercise.Models;

namespace AutoMappingExercise.Core.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        private readonly AutoMappingExerciseContext context;
        private readonly IMapper mapper;

        public EmployeeController(AutoMappingExerciseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = mapper.Map<Employee>(employeeDto);

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            Employee employee = context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException();
            }

            employee.Birthday = date;
            context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            Employee employee = context.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new ArgumentException();
            }

            employee.Address = address;
            context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {

            Employee employee = context.Employees.Find(employeeId);
            EmployeeDto employeeDto = mapper.Map<EmployeeDto>(employee);

            if (employee == null)
            {
                throw new  ArgumentException();
            }

            return employeeDto;
        }

        public EmployeeFullDto GetFullEmployeeInfo(int employeeId)
        {
            Employee employee = context.Employees.Find(employeeId);
            EmployeeFullDto employeeDto = mapper.Map<EmployeeFullDto>(employee);

            if (employee == null)
            {
                throw new ArgumentException();
            }

            return employeeDto;
        }
    }
}
