using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthWindCRUD.Models;
using NorthWindCRUD.Dtos;

namespace NorthWindCRUD.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}