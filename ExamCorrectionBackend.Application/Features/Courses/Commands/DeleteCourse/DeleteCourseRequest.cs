using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseRequest : IRequest<CourseDto>
    {
        public int CourseId { get; set; }
        public string OwnerId { get; set; }
    }
}