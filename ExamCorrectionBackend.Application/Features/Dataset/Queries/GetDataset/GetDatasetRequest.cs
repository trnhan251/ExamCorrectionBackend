using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Queries.GetDataset
{
    public class GetDatasetRequest : IRequest<DatasetDto>
    {
        public int DatasetId { get; set; }
    }
}