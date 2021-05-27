using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Queries.GetAllCourses
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesRequest, List<CourseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _repository;

        public GetAllCoursesHandler(IMapper mapper, ICourseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<CourseDto>> Handle(GetAllCoursesRequest request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllByOwnerId(request.OwnerId);
            return _mapper.Map<List<CourseDto>>(entities);
        }
    }
}