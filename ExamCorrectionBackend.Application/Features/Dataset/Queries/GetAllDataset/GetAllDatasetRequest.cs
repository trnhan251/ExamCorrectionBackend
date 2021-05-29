using System.Collections.Generic;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Queries.GetAllDataset
{
    public class GetAllDatasetRequest : IRequest<List<DatasetDto>>
    {
        
    }
}