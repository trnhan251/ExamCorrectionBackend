using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.Dataset.Commands.CreateDatasetFromStudentSolution;
using ExamCorrectionBackend.Application.Features.ExamTasks.Commands.CreateExamTask;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetAllExamTasksFromExam;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetExamTask;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.CreateStudentSolution;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.DeleteStudentSolution;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.UpdateStudentSolution;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetAllStudentSolutionsFromExamTask;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Queries.GetStudentSolution;
using ExamCorrectionBackend.Models;
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
    public class StudentSolutionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentSolutionsController(IMediator mediator, IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment environment, IHttpClientFactory httpClientFactory)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/<StudentSolutionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentSolutionDto>>> GetAllFromExamTask([FromQuery] int examTaskId)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetAllStudentSolutionsFromExamTaskRequest() {ExamTaskId = examTaskId, UserId = userId};
            var results = await _mediator.Send(request);
            return results != null ? (ActionResult<IEnumerable<StudentSolutionDto>>) Ok(results) : BadRequest();
        }

        // GET api/<StudentSolutionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentSolutionDto>> Get(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new GetStudentSolutionRequest() { StudentSolutionId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<StudentSolutionDto>) Ok(result) : BadRequest();
        }

        // POST api/<StudentSolutionsController>
        [HttpPost]
        public async Task<ActionResult<StudentSolutionDto>> Post([FromBody] StudentSolutionDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new CreateStudentSolutionRequest() { StudentSolutionDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<StudentSolutionDto>) Ok(result) : BadRequest();
        }

        // PUT api/<StudentSolutionsController>/5
        [HttpPut]
        public async Task<ActionResult<StudentSolutionDto>> Put([FromBody] StudentSolutionDto dto)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new UpdateStudentSolutionRequest() { StudentSolutionDto = dto, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<StudentSolutionDto>) Ok(result) : BadRequest();
        }

        // DELETE api/<StudentSolutionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentSolutionDto>> Delete(int id)
        {
            var userId = GetUserIdFromHttpContext();
            var request = new DeleteStudentSolutionRequest() { StudentSolutionId = id, UserId = userId };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<StudentSolutionDto>) Ok(result) : BadRequest();
        }

        [HttpPost("Excel")]
        public async Task<ActionResult<IEnumerable<StudentSolutionDto>>> UploadExcel([FromQuery] int examId, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            try
            {
                var studentSolutionResults = new List<StudentSolutionDto>();

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

                    var examTasks = await _mediator.Send(new GetAllExamTasksFromExamRequest()
                        { ExamId = examId, UserId = GetUserIdFromHttpContext() });

                    var studentSolutions = 
                        this.GetStudentSolutions(_environment.WebRootPath + "\\Upload\\" + file.FileName, examTasks);

                    var userId = GetUserIdFromHttpContext();

                    foreach (var studentSolution in studentSolutions)
                    {
                        var request = new CreateStudentSolutionRequest() { StudentSolutionDto = studentSolution, UserId = userId };
                        var result = await _mediator.Send(request);
                        studentSolutionResults.Add(result);
                    }
                }

                return Ok(studentSolutionResults);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<StudentSolutionDto> GetStudentSolutions(string fileName, List<ExamTaskDto> examTasks)
        {
            var studentSolutionDtos = new List<StudentSolutionDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            while (reader.Read())
            {
                var taskId = examTasks.Find(x =>
                    x.TaskOrder == Int32.Parse(reader.GetValue(0).ToString() ?? throw new InvalidOperationException()))?.Id;

                if (taskId != null)
                    studentSolutionDtos.Add(new StudentSolutionDto()
                    {
                        TaskId = (int) taskId,
                        StudentId = reader.GetValue(1).ToString(),
                        Answer = reader.GetValue(2).ToString()
                    });
            }

            return studentSolutionDtos;
        }

        [HttpPost("{id}/Score")]
        public async Task<ActionResult<StudentSolutionDto>> ScoreStudentSolution(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var studentSolution = await _mediator.Send(new GetStudentSolutionRequest()
                { StudentSolutionId = id, UserId = GetUserIdFromHttpContext() });
            var examTask = await _mediator.Send(new GetExamTaskRequest()
                { ExamTaskId = studentSolution.TaskId, UserId = GetUserIdFromHttpContext() });

            var scoreRequestObject = new ScoreRequest()
                { Sentence1 = examTask.Solution, Sentence2 = studentSolution.Answer };
            var jsonObject = new StringContent(JsonSerializer.Serialize(scoreRequestObject), Encoding.UTF8,
                "application/json");

            using var httpResponse = await client.PostAsync("http://localhost:5000/api/predict", jsonObject);
            httpResponse.EnsureSuccessStatusCode();
            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            var number = responseBody.Split('[')[1].Split(']')[0];
            var scoreResult = Decimal.Parse(number, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"));

            studentSolution.Score = (scoreResult/5)*100;
            var result = await _mediator.Send(new UpdateStudentSolutionRequest()
                { StudentSolutionDto = studentSolution, UserId = GetUserIdFromHttpContext() });
            return result != null ? (ActionResult<StudentSolutionDto>) Ok(result) : BadRequest();
        }

        [HttpPost("{id}/AddIntoDataset")]
        public async Task<ActionResult<DatasetDto>> AddIntoDataset(int id)
        {
            var request = new CreateDatasetFromStudentSolutionRequest()
                {StudentSolutionId = id, UserId = GetUserIdFromHttpContext()};
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<DatasetDto>) Ok(result) : BadRequest();
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
