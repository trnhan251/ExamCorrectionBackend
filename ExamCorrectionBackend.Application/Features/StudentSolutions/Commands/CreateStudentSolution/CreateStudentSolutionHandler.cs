using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.CreateStudentSolution
{
    public class CreateStudentSolutionHandler : IRequestHandler<CreateStudentSolutionRequest, StudentSolutionDto>
    {
        private readonly IMapper _mapper;
        private readonly IStudentSolutionRepository _repository;
        private readonly IExamTaskRepository _examTaskRepository;

        public CreateStudentSolutionHandler(IMapper mapper, IStudentSolutionRepository repository, IExamTaskRepository examTaskRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _examTaskRepository = examTaskRepository;
        }

        public async Task<StudentSolutionDto> Handle(CreateStudentSolutionRequest request, CancellationToken cancellationToken)
        {
            var dto = request.StudentSolutionDto;
            var examTask = await _examTaskRepository.GetByIdIncludedExam(dto.TaskId);

            if (examTask == null)
                throw new Exception("Cannot find exam task with ID = " + dto.TaskId);

            if (!examTask.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam task");

            var entity = new StudentSolution()
            {
                TaskId = examTask.Id,
                StudentId = dto.StudentId,
                Answer = dto.Answer
            };

            var result = await _repository.AddAsync(entity);

            return _mapper.Map<StudentSolutionDto>(result);
        }
    }
}