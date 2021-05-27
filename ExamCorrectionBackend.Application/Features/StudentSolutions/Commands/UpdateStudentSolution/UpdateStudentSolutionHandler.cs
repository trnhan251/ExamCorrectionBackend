using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.UpdateStudentSolution
{
    public class UpdateStudentSolutionHandler : IRequestHandler<UpdateStudentSolutionRequest, StudentSolutionDto>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSolutionRepository _repository;

        public UpdateStudentSolutionHandler(IMapper mapper, IStudentSolutionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<StudentSolutionDto> Handle(UpdateStudentSolutionRequest request, CancellationToken cancellationToken)
        {
            var dto = request.StudentSolutionDto;
            var entity = await _repository.GetByIdIncludedExamTask(dto.Id);

            if (entity == null)
                throw new Exception("Cannot find student solution with ID = " + dto.Id);

            if (!entity.Task.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this student solution");

            entity.Answer = dto.Answer;
            entity.StudentId = dto.StudentId;

            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<StudentSolutionDto>(result);
        }
    }
}