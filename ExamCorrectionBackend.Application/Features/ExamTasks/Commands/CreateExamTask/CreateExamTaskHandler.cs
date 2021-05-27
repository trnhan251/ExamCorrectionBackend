using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Commands.CreateExamTask
{
    public class CreateExamTaskHandler : IRequestHandler<CreateExamTaskRequest, ExamTaskDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamTaskRepository _repository;
        private readonly IExamRepository _examRepository;

        public CreateExamTaskHandler(IMapper mapper, IExamTaskRepository repository, IExamRepository examRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _examRepository = examRepository;
        }

        public async Task<ExamTaskDto> Handle(CreateExamTaskRequest request, CancellationToken cancellationToken)
        {
            var dto = request.ExamTaskDto;
            
            var examEntity = await _examRepository.GetByIdIncludedCourse(dto.ExamId);

            if (examEntity == null)
                throw new Exception("Cannot find exam with ID = " + dto.ExamId);

            if (!examEntity.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam");

            var entity = new ExamTask()
            {
                ExamId = dto.ExamId,
                Description = dto.Description,
                Solution = dto.Solution
            };

            var result = await _repository.AddAsync(entity);
            return _mapper.Map<ExamTaskDto>(result);
        }
    }
}