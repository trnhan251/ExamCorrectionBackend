using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.DeleteDataset
{
    public class DeleteDatesetHandler : IRequestHandler<DeleteDatasetRequest, DatasetDto>
    {
        private readonly IMapper _mapper;
        private readonly IDatasetRepository _repository;

        public DeleteDatesetHandler(IMapper mapper, IDatasetRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DatasetDto> Handle(DeleteDatasetRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.DatasetId);

            if (entity == null)
                throw new Exception("Cannot find dataset with ID = " + request.DatasetId);

            var result = await _repository.DeleteAsync(entity);
            return _mapper.Map<DatasetDto>(result);
        }
    }
}