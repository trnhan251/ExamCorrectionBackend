using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.CreateStudentSolution
{
    public class CreateStudentSolutionRequest : IRequest<StudentSolutionDto>
    {
        public StudentSolutionDto StudentSolutionDto { get; set; }
        public string UserId { get; set; }
    }
}