using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Queries.GetCourse
{
    public class GetCourseRequest : IRequest<CourseDto>
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }
}