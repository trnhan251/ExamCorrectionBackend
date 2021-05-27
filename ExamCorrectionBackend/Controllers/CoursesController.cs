using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Features.Courses.Queries.GetAllCourses;
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
            var userId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                userId = _httpContextAccessor.HttpContext.User.GetObjectId();
            }

            var request = new GetAllCoursesRequest() {OwnerId = userId};
            var results = await _mediator.Send(request);
            return results != null ? Ok(results) : BadRequest();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoursesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
