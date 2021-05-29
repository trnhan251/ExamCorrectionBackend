using System.Threading;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.ScoreStudentSolution
{
    public class ScoreStudentSolutionHandler : IRequestHandler<ScoreStudentSolutionRequest, ScoreStudentSolutionDto>
    {
        public ScoreStudentSolutionHandler()
        {
        }

        public Task<ScoreStudentSolutionDto> Handle(ScoreStudentSolutionRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}