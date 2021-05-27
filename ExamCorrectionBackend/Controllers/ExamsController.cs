using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.Exams.Commands.CreateExam;
using ExamCorrectionBackend.Application.Features.Exams.Commands.DeleteExam;
using ExamCorrectionBackend.Application.Features.Exams.Commands.UpdateExam;
using ExamCorrectionBackend.Application.Features.Exams.Queries.GetAllExams;
using ExamCorrectionBackend.Application.Features.Exams.Queries.GetExam;
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
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExamsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<ExamsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> Get()
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetAllExamsRequest() {UserId = userId};
            var results = await _mediator.Send(request);
            return results != null ? Ok(results) : BadRequest();
        }

        // GET api/<ExamsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDto>> Get(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetExamRequest() { ExamId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // POST api/<ExamsController>
        [HttpPost]
        public async Task<ActionResult<ExamDto>> Post([FromBody] ExamDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new CreateExamRequest() { ExamDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // PUT api/<ExamsController>/5
        [HttpPut]
        public async Task<ActionResult<ExamDto>> Put([FromBody] ExamDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new UpdateExamRequest() { ExamDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // DELETE api/<ExamsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamDto>> Delete(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new DeleteExamRequest() { ExamId = id, UserId = userId };
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
