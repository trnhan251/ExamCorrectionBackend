using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetAllExamTasksFromExam
{
    public class GetAllExamTasksFromExamHandler : IRequestHandler<GetAllExamTasksFromExamRequest, List<ExamTaskDto>>
    {
        private readonly IMapper _mapper;
        private readonly IExamTaskRepository _repository;
        private readonly IExamRepository _examRepository;

        public GetAllExamTasksFromExamHandler(IMapper mapper, IExamTaskRepository repository, IExamRepository examRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _examRepository = examRepository;
        }

        public async Task<List<ExamTaskDto>> Handle(GetAllExamTasksFromExamRequest request, CancellationToken cancellationToken)
        {
            var examEntity = await _examRepository.GetByIdIncludedCourse(request.ExamId);

            if (examEntity == null)
                throw new Exception("Cannot find exam with ID = " + request.ExamId);

            if (!examEntity.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam");

            var entities = await _repository.GetExamTasksFromExamId(examEntity.Id);

            return _mapper.Map<List<ExamTaskDto>>(entities);
        }
    }
}