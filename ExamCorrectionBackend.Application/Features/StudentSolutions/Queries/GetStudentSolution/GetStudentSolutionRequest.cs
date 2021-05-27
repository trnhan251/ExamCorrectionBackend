using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetStudentSolution
{
    public class GetStudentSolutionRequest : IRequest<StudentSolutionDto>
    {
        public int StudentSolutionId { get; set; }
        public string UserId { get; set; }
    }
}