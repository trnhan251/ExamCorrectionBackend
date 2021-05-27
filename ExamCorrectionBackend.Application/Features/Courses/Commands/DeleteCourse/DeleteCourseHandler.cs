using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseHandler : IRequestHandler<DeleteCourseRequest, CourseDto>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _repository;

        public DeleteCourseHandler(IMapper mapper, ICourseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CourseDto> Handle(DeleteCourseRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.CourseId);

            if (entity == null)
                throw new Exception("Cannot find course with ID = " + request.CourseId);

            if (!entity.OwnerId.Equals(request.OwnerId))
                throw new Exception("Not allowed to update this course");

            var result = _repository.DeleteAsync(entity);
            return _mapper.Map<CourseDto>(result);
        }
    }
}