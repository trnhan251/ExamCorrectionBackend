using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Queries.GetCourse
{
    public class GetCourseHandler : IRequestHandler<GetCourseRequest, CourseDto>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _repository;

        public GetCourseHandler(IMapper mapper, ICourseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CourseDto> Handle(GetCourseRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.CourseId);

            if (entity == null)
                throw new Exception("Cannot find course with ID = " + request.CourseId);

            if (!entity.OwnerId.Equals(request.UserId))
                throw new Exception("Not allowed to read this course");

            return _mapper.Map<CourseDto>(entity);
        }
    }
}