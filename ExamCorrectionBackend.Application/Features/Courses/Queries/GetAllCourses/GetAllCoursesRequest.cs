using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesRequest : IRequest<List<CourseDto>>
    {
        public string OwnerId { get; set; }
    }
}