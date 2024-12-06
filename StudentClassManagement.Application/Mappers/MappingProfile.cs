using AutoMapper;
using StudentClassManagement.Core.DTOs;
using StudentClassManagement.Core.Entities;

namespace StudentClassManagement.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Class, ClassDto>().ReverseMap();
    }
}