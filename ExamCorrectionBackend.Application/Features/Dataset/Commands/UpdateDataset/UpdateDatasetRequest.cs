using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.UpdateDataset
{
    public class UpdateDatasetRequest : IRequest<DatasetDto>
    {
        public DatasetDto DatasetDto { get; set; }
    }
}