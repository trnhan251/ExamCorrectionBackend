using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Queries.GetDataset
{
    public class GetDatasetHandler : IRequestHandler<GetDatasetRequest, DatasetDto>
    {
        private readonly IMapper _mapper;
        private readonly IDatasetRepository _repository;

        public GetDatasetHandler(IMapper mapper, IDatasetRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DatasetDto> Handle(GetDatasetRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.DatasetId);
            return _mapper.Map<DatasetDto>(entity);
        }
    }
}