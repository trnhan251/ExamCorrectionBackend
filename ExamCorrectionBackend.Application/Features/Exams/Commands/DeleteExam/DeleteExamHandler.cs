using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Commands.DeleteExam
{
    public class DeleteExamHandler : IRequestHandler<DeleteExamRequest, ExamDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamRepository _repository;

        public DeleteExamHandler(IMapper mapper, IExamRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExamDto> Handle(DeleteExamRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdIncludedCourse(request.ExamId);

            if (entity == null)
                throw new Exception("Cannot find exam with ID = " + request.ExamId);

            if (!entity.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to update this exam");

            var result = await _repository.DeleteAsync(entity);

            return _mapper.Map<ExamDto>(result);
        }
    }
}