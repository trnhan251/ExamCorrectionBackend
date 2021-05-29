using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.ScoreStudentSolution
{
    public class ScoreStudentSolutionRequest : IRequest<ScoreStudentSolutionDto>
    {
        public int StudentSolutionId { get; set; }
        public string UserId { get; set; }
    }
}