using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Queries.GetExam
{
    public class GetExamHandler : IRequestHandler<GetExamRequest, ExamDto>
    {
        private readonly IMapper _mapper;
        private readonly IExamRepository _repository;

        public GetExamHandler(IMapper mapper, IExamRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ExamDto> Handle(GetExamRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdIncludedCourse(request.ExamId);

            if (entity == null)
                throw new Exception("Cannot find exam with ID = " + request.ExamId);

            if (!entity.Course.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this exam");

            return _mapper.Map<ExamDto>(entity);
        }
    }
}