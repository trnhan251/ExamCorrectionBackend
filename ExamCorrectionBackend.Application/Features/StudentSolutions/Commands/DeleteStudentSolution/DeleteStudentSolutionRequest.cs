using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.DeleteStudentSolution
{
    public class DeleteStudentSolutionRequest : IRequest<StudentSolutionDto>
    {
        public int StudentSolutionId { get; set; }
        public string UserId { get; set; }
    }
}