using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Exams.Queries.GetAllExams
{
    public class GetAllExamsHandler : IRequestHandler<GetAllExamsRequest, List<ExamDto>>
    {
        private readonly IMapper _mapper;
        private readonly IExamRepository _repository;

        public GetAllExamsHandler(IMapper mapper, IExamRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<ExamDto>> Handle(GetAllExamsRequest request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllByOwnerId(request.UserId);
            return _mapper.Map<List<ExamDto>>(entities);
        }
    }
}