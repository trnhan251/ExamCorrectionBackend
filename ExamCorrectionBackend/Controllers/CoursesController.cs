using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.Courses.Commands.CreateCourse;
using ExamCorrectionBackend.Application.Features.Courses.Commands.DeleteCourse;
using ExamCorrectionBackend.Application.Features.Courses.Commands.UpdateCourse;
using ExamCorrectionBackend.Application.Features.Courses.Queries.GetAllCourses;
using ExamCorrectionBackend.Application.Features.Courses.Queries.GetCourse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamCorrectionBackend.Controllers
{
    [EnableCors]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CoursesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetAllCoursesRequest() {OwnerId = userId};
            var results = await _mediator.Send(request);
            return results != null ? (ActionResult<IEnumerable<string>>) Ok(results) : BadRequest();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetCourseRequest() {CourseId = id, UserId = userId};
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<CourseDto>) Ok(result) : BadRequest();
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Post([FromBody] CourseDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new CreateCourseRequest() {CourseDto = dto, OwnerId = userId};
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<CourseDto>) Ok(result) : BadRequest();
        }

        // PUT api/<CoursesController>/5
        [HttpPut]
        public async Task<ActionResult<CourseDto>> Put([FromBody] CourseDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new UpdateCourseRequest() { CourseDto = dto, OwnerId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<CourseDto>) Ok(result) : BadRequest();
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseDto>> Delete(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new DeleteCourseRequest() { CourseId = id, OwnerId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<CourseDto>) Ok(result) : BadRequest();
        }

        private string GetUserIdFromHttpContext()
        {
            var userId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                userId = _httpContextAccessor.HttpContext.User.GetObjectId();
            }

            return userId;
        }
    }
}
