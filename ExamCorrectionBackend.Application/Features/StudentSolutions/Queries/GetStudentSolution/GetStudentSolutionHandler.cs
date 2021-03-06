using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetStudentSolution
{
    public class GetStudentSolutionHandler : IRequestHandler<GetStudentSolutionRequest, StudentSolutionDto>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSolutionRepository _repository;

        public GetStudentSolutionHandler(IMapper mapper, IStudentSolutionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<StudentSolutionDto> Handle(GetStudentSolutionRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdIncludedExamTask(request.StudentSolutionId);

            if (entity == null)
                throw new Exception("Cannot find student solution with ID = " + request.StudentSolutionId);

            if (!entity.Task.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this student solution");

            return _mapper.Map<StudentSolutionDto>(entity);
        }
    }
}