using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using XmlProcessingExercise.App.Dto;
using XmlProcessingExercise.Models;

namespace XmlProcessingExercise.App
{
    public class XmlProcessingExerciseProfile : Profile
    {
        public XmlProcessingExerciseProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
