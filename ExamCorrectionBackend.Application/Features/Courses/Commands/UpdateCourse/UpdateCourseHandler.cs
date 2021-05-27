using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExamCorrectionBackend.Application.Contracts.Persistence;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Domain.Entities;
using MediatR;

namespace ExamCorrectionBackend.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseRequest, CourseDto>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _repository;

        public UpdateCourseHandler(IMapper mapper, ICourseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CourseDto> Handle(UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.CourseDto.Id);

            if (entity == null)
                throw new Exception("Cannot find course with ID = " + request.CourseDto.Id);

            if (!entity.OwnerId.Equals(request.OwnerId))
                throw new Exception("Not allowed to update this course");

            entity.Name = request.CourseDto.Name;
            entity.Description = request.CourseDto.Description;

            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<CourseDto>(result);
        }
    }
}