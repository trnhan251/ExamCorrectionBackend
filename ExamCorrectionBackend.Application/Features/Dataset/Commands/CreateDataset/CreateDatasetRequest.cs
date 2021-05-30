using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.CreateDataset
{
    public class CreateDatasetRequest : IRequest<DatasetDto>
    {
        public DatasetDto DatasetDto { get; set; }
    }
}