using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.CreateExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.DeleteExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.UpdateExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetAllExamTasksFromExam;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetExamTask;
using ExamCorrectionBackend.Domain.Entities;
using ExamCorrectionBackend.Utilities;
using ExcelDataReader;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _environment;

        public ExamTasksController(IMediator mediator, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        // GET: api/<ExamTasksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamTaskDto>>> GetAllFromExam([FromQuery] int examId)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetAllExamTasksFromExamRequest() {ExamId = examId, UserId = userId};
            var results = await _mediator.Send(request);
            return results != null ? (ActionResult<IEnumerable<ExamTaskDto>>) Ok(results) : BadRequest();
        }

        // GET api/<ExamTasksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamTaskDto>> Get(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetExamTaskRequest() { ExamTaskId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<ExamTaskDto>) Ok(result) : BadRequest();
        }

        // POST api/<ExamTasksController>
        [HttpPost]
        public async Task<ActionResult<ExamTaskDto>> Post([FromBody] ExamTaskDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new CreateExamTaskRequest() { ExamTaskDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<ExamTaskDto>) Ok(result) : BadRequest();
        }

        // PUT api/<ExamTasksController>/5
        [HttpPut]
        public async Task<ActionResult<ExamTaskDto>> Put([FromBody] ExamTaskDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new UpdateExamTaskRequest() { ExamTaskDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<ExamTaskDto>) Ok(result) : BadRequest();
        }

        // DELETE api/<ExamTasksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamTaskDto>> Delete(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new DeleteExamTaskRequest() { ExamTaskId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<ExamTaskDto>) Ok(result) : BadRequest();
        }

        [HttpPost("Excel")]
        public async Task<ActionResult<IEnumerable<ExamTaskDto>>> UploadExcel([FromQuery] int examId, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            try
            {
                var examTaskResults = new List<ExamTaskDto>();

                foreach (var file in Request.Form.Files.OfType<IFormFile>())
                {
                    if (file.Length <= 0)
                        return null;

                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload");
                    }
                    await using FileStream fileStream =
                        System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + file.FileName);
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                    fileStream.Close();

                    var examTasks = this.GetExamTasks(_environment.WebRootPath + "\\Upload\\" + file.FileName, examId);
                    var userId = GetUserIdFromHttpContext();

                    foreach (var examTask in examTasks)
                    {
                        var request = new CreateExamTaskRequest() {ExamTaskDto = examTask, UserId = userId};
                        var result = await _mediator.Send(request);
                        examTaskResults.Add(result);
                    }
                }

                return Ok(examTaskResults);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<ExamTaskDto> GetExamTasks(string fileName, int examId)
        {
            var examTasks = new List<ExamTaskDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            while (reader.Read())
            {
                examTasks.Add(new ExamTaskDto()
                {
                    ExamId = examId,
                    Description = reader.GetValue(0).ToString(),
                    Solution = reader.GetValue(1).ToString()
                });
            }

            return examTasks;
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
