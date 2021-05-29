using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.DeleteDataset
{
    public class DeleteDatasetRequest : IRequest<DatasetDto>
    {
        public int DatasetId { get; set; }
    }
}