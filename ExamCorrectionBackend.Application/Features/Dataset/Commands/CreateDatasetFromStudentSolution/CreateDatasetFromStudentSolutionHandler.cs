using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Dataset.Commands.CreateDatasetFromStudentSolution
{
    public class CreateDatasetFromStudentSolutionHandler : IRequestHandler<CreateDatasetFromStudentSolutionRequest, DatasetDto>
    {
        private readonly IMapper _mapper;
        private readonly IDatasetRepository _repository;
        private readonly IStudentSolutionRepository _studentSolutionRepository;

        public CreateDatasetFromStudentSolutionHandler(IMapper mapper, IDatasetRepository repository, IStudentSolutionRepository studentSolutionRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _studentSolutionRepository = studentSolutionRepository;
        }

        public async Task<DatasetDto> Handle(CreateDatasetFromStudentSolutionRequest request, CancellationToken cancellationToken)
        {
            var studentSolution = await _studentSolutionRepository.GetByIdIncludedExamTask(request.StudentSolutionId);

            if (studentSolution == null)
                throw new Exception("Cannot find student solution with ID = " + request.StudentSolutionId);

            if (!studentSolution.Task.Exam.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this student solution");

            if (studentSolution.Score == null)
                throw new Exception("Cannot add a null score into dataset");

            var entity = new Domain.Entities.Dataset()
            {
                Sentence1 = studentSolution.Task.Solution,
                Sentence2 = studentSolution.Answer,
                CreatedDate = DateTime.Now,
                Score = (decimal) studentSolution.Score,
                IsSimilar = studentSolution.Score > studentSolution.Task.Exam.ScoreThreshold
            };

            var result = await _repository.AddAsync(entity);
            return _mapper.Map<DatasetDto>(result);
        }
    }
}