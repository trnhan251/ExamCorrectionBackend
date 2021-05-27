using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.CreateStudentSolution;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.DeleteStudentSolution;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.UpdateStudentSolution;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetAllStudentSolutionsFromExamTask;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetStudentSolution;
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
    public class StudentSolutionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentSolutionsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<StudentSolutionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSolutionDto>>> GetAllFromExamTask([FromQuery] int examTaskId)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetAllStudentSolutionsFromExamTaskRequest() {ExamTaskId = examTaskId, UserId = userId};
            var results = await _mediator.Send(request);
            return results != null ? Ok(results) : BadRequest();
        }

        // GET api/<StudentSolutionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSolutionDto>> Get(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetStudentSolutionRequest() { StudentSolutionId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // POST api/<StudentSolutionsController>
        [HttpPost]
        public async Task<ActionResult<StudentSolutionDto>> Post([FromBody] StudentSolutionDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new CreateStudentSolutionRequest() { StudentSolutionDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // PUT api/<StudentSolutionsController>/5
        [HttpPut]
        public async Task<ActionResult<StudentSolutionDto>> Put([FromBody] StudentSolutionDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new UpdateStudentSolutionRequest() { StudentSolutionDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // DELETE api/<StudentSolutionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentSolutionDto>> Delete(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new DeleteStudentSolutionRequest() { StudentSolutionId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
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
