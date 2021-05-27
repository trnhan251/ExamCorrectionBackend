using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Commands.DeleteExamTask
{
    public class DeleteExamTaskHandler : IRequestHandler<DeleteExamTaskRequest, ExamTaskDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamTaskRepository _repository;

        public DeleteExamTaskHandler(IMapper mapper, IExamTaskRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExamTaskDto> Handle(DeleteExamTaskRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdIncludedExam(request.ExamTaskId);

            if (entity == null)
                throw new Exception("Cannot find exam task with ID = " + request.ExamTaskId);

            if (!entity.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam task");

            var result = await _repository.DeleteAsync(entity);
            
            return _mapper.Map<ExamTaskDto>(result);
        }
    }
}