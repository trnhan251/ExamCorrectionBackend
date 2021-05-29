using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Queries.GetAllDataset
{
    public class GetAllDatasetHandler : IRequestHandler<GetAllDatasetRequest, List<DatasetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDatasetRepository _repository;

        public GetAllDatasetHandler(IMapper mapper, IDatasetRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<DatasetDto>> Handle(GetAllDatasetRequest request, CancellationToken cancellationToken)
        {
            var entities = await _repository.ListAllAsync();
            return _mapper.Map<List<DatasetDto>>(entities);
        }
    }
}