using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Commands.UpdateExam
{
    public class UpdateExamHandler : IRequestHandler<UpdateExamRequest, ExamDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamRepository _repository;

        public UpdateExamHandler(IMapper mapper, IExamRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExamDto> Handle(UpdateExamRequest request, CancellationToken cancellationToken)
        {
            var dto = request.ExamDto;
            var entity = await _repository.GetByIdIncludedCourse(dto.Id);

            if (entity == null)
                throw new Exception("Cannot find exam with ID = " + dto.Id);

            if (!entity.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to update this exam");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Date = dto.Date;
            entity.ScoreThreshold = dto.ScoreThreshold;

            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<ExamDto>(result);
        }
    }
}