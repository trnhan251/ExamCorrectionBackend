using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.UpdateDataset
{
    public class UpdateDatasetHandler : IRequestHandler<UpdateDatasetRequest, DatasetDto>
    {
        private readonly IMapper _mapper;
        private readonly IDatasetRepository _repository;

        public UpdateDatasetHandler(IMapper mapper, IDatasetRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DatasetDto> Handle(UpdateDatasetRequest request, CancellationToken cancellationToken)
        {
            var dto = request.DatasetDto;
            var entity = await _repository.GetByIdAsync(dto.Id);

            if (entity == null)
                throw new Exception("Cannot find dataset with ID = " + dto.Id);

            entity.Sentence1 = dto.Sentence1;
            entity.Sentence2 = dto.Sentence2;
            entity.CreatedDate = DateTime.Now;
            entity.Score = dto.Score;
            entity.IsSimilar = dto.IsSimilar;

            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<DatasetDto>(result);
        }
    }
}