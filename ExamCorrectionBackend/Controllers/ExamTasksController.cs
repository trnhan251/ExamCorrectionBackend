using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.CreateExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.DeleteExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.UpdateExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetAllExamTasksFromExam;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetExamTask;
using ExamCorrectionBackend.Domain.Entities;
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
    public class ExamTasksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExamTasksController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<ExamTasksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamTaskDto>>> GetAllFromExam([FromQuery] int examId)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetAllExamTasksFromExamRequest() {ExamId = examId, UserId = userId};
            var results = await _mediator.Send(request);
            return results != null ? Ok(results) : BadRequest();
        }

        // GET api/<ExamTasksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamTaskDto>> Get(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetExamTaskRequest() { ExamTaskId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // POST api/<ExamTasksController>
        [HttpPost]
        public async Task<ActionResult<ExamTaskDto>> Post([FromBody] ExamTaskDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new CreateExamTaskRequest() { ExamTaskDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // PUT api/<ExamTasksController>/5
        [HttpPut]
        public async Task<ActionResult<ExamTaskDto>> Put([FromBody] ExamTaskDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new UpdateExamTaskRequest() { ExamTaskDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // DELETE api/<ExamTasksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamTaskDto>> Delete(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new DeleteExamTaskRequest() { ExamTaskId = id, UserId = userId };
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
