using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.CreateDatasetFromStudentSolution
{
    public class CreateDatasetFromStudentSolutionRequest : IRequest<DatasetDto>
    {
        public int StudentSolutionId { get; set; }
        public string UserId { get; set; }
    }
}