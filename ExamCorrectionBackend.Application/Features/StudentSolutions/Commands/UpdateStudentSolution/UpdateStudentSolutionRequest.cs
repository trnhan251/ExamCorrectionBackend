using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.UpdateStudentSolution
{
    public class UpdateStudentSolutionRequest : IRequest<StudentSolutionDto>
    {
        public StudentSolutionDto StudentSolutionDto { get; set; }
        public string UserId { get; set; }
    }
}