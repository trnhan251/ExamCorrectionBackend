using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseRequest, CourseDto>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _repository;

        public CreateCourseHandler(IMapper mapper, ICourseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CourseDto> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var dto = request.CourseDto;
            var course = new Course()
            {
                Name = dto.Name,
                Description = dto.Description,
                OwnerId = request.OwnerId
            };
            var entity = await _repository.AddAsync(course);
            return _mapper.Map<CourseDto>(entity);
        }
    }
}