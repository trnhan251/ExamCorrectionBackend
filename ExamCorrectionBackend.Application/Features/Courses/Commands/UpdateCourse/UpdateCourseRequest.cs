using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseRequest : IRequest<CourseDto>
    {
        public CourseDto CourseDto { get; set; }
        public string OwnerId { get; set; }
    }
}