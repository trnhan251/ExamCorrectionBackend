using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetExamTask
{
    public class GetExamTaskHandler : IRequestHandler<GetExamTaskRequest, ExamTaskDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamTaskRepository _repository;
        private readonly IExamRepository _examRepository;

        public GetExamTaskHandler(IMapper mapper, IExamTaskRepository repository, IExamRepository examRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _examRepository = examRepository;
        }

        public async Task<ExamTaskDto> Handle(GetExamTaskRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdIncludedExam(request.ExamTaskId);

            if (entity == null)
                throw new Exception("Cannot find exam task with ID = " + request.ExamTaskId);

            if (!entity.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam task");

            return _mapper.Map<ExamTaskDto>(entity);
        }
    }
}