using System;
using System.Collections.Generic;
using System.Text;
using AutoMappingExercise.Core.Dto;
using AutoMappingExercise.Models;

namespace AutoMappingExercise.Core
{
    public class AutoMappingExerciseProfile : AutoMapper.Profile
    {
        public AutoMappingExerciseProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
