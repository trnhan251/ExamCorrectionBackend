using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.CreateDataset
{
    public class CreateDatasetHandler : IRequestHandler<CreateDatasetRequest, DatasetDto>
    {
        private readonly IMapper _mapper;
        private readonly IDatasetRepository _repository;

        public CreateDatasetHandler(IMapper mapper, IDatasetRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DatasetDto> Handle(CreateDatasetRequest request, CancellationToken cancellationToken)
        {
            var dto = request.DatasetDto;
            var entity = new Domain.Entities.Dataset()
            {
                Sentence1 = dto.Sentence1,
                Sentence2 = dto.Sentence2,
                Score = dto.Score,
                IsSimilar = dto.IsSimilar,
                CreatedDate = DateTime.Now
            };
            var result = await _repository.AddAsync(entity);
            return _mapper.Map<DatasetDto>(result);
        }
    }
}