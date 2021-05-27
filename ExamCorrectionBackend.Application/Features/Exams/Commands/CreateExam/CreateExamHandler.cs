using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamHandler : IRequestHandler<CreateExamRequest, ExamDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamRepository _repository;
        private readonly ICourseRepository _courseRepository;

        public CreateExamHandler(IMapper mapper, IExamRepository repository, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _courseRepository = courseRepository;
        }

        public async Task<ExamDto> Handle(CreateExamRequest request, CancellationToken cancellationToken)
        {
            var courseEntity = await _courseRepository.GetByIdAsync(request.ExamDto.CourseId);

            if (courseEntity == null)
                throw new Exception("Cannot find course with ID = " + request.ExamDto.CourseId);

            if (!courseEntity.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to create an exam for this course");

            var dto = request.ExamDto;
            var exam = new Exam()
            {
                Name = dto.Name,
                Description = dto.Description,
                CourseId = dto.CourseId,
                Date = dto.Date,
                ScoreThreshold = dto.ScoreThreshold,
            };

            var entity = await _repository.AddAsync(exam);
            return _mapper.Map<ExamDto>(entity);
        }
    }
}