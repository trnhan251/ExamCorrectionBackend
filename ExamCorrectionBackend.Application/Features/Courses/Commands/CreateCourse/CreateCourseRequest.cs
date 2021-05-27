using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseRequest : IRequest<CourseDto>
    {
        public CourseDto CourseDto { get; set; }
        public string OwnerId { get; set; }
    }
}