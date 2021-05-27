using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetAllStudentSolutionsFromExamTask
{
    public class GetAllStudentSolutionsFromExamTaskHandler : IRequestHandler<GetAllStudentSolutionsFromExamTaskRequest, List<StudentSolutionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSolutionRepository _repository;
        private readonly IExamTaskRepository _examTaskRepository;

        public GetAllStudentSolutionsFromExamTaskHandler(IMapper mapper, IStudentSolutionRepository repository, 
            IExamTaskRepository examTaskRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _examTaskRepository = examTaskRepository;
        }

        public async Task<List<StudentSolutionDto>> Handle(GetAllStudentSolutionsFromExamTaskRequest request, CancellationToken cancellationToken)
        {
            var examTaskEntity = await _examTaskRepository.GetByIdIncludedExam(request.ExamTaskId);

            if (examTaskEntity == null)
                throw new Exception("Cannot find exam task with ID = " + request.ExamTaskId);

            if (!examTaskEntity.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to see this exam task");

            var entities = await _repository.GetAllFromExamTaskId(examTaskEntity.Id);

            return _mapper.Map<List<StudentSolutionDto>>(entities);
        }
    }
}