using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetAllStudentSolutionsFromExamTask
{
    public class GetAllStudentSolutionsFromExamTaskRequest : IRequest<List<StudentSolutionDto>>
    {
        public int ExamTaskId { get; set; }
        public string UserId { get; set; }
    }
}