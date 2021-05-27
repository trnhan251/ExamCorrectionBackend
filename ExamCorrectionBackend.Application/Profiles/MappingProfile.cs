using AutoMapper;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;

namespace ExamCorrectionBackend.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
        }
    }
}