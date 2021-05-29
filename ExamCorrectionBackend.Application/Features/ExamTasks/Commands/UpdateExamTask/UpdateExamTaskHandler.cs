using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Commands.UpdateExamTask
{
    public class UpdateExamTaskHandler : IRequestHandler<UpdateExamTaskRequest, ExamTaskDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamTaskRepository _repository;

        public UpdateExamTaskHandler(IMapper mapper, IExamTaskRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExamTaskDto> Handle(UpdateExamTaskRequest request, CancellationToken cancellationToken)
        {
            var dto = request.ExamTaskDto;

            var entity = await _repository.GetByIdIncludedExam(dto.Id);

            if (entity == null)
                throw new Exception("Cannot find exam task with ID = " + dto.Id);

            if (!entity.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam task");

            entity.Description = dto.Description;
            entity.Solution = dto.Solution;
            entity.TaskOrder = dto.TaskOrder;

            var result = await _repository.UpdateAsync(entity);
            
            return _mapper.Map<ExamTaskDto>(result);
        }
    }
}