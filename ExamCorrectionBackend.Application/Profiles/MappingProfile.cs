using System.Diagnostics.Tracing;
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
            CreateMap<Exam, ExamDto>()
                .ForMember(des => des.CourseName, opt => opt.MapFrom(res => res.Course.Name));
            CreateMap<ExamTask, ExamTaskDto>();
            CreateMap<StudentSolution, StudentSolutionDto>();
            CreateMap<Dataset, DatasetDto>();
        }
    }
}