using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.Dataset.Commands.CreateDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Commands.DeleteDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Commands.UpdateDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Queries.GetAllDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Queries.GetDataset;
using ExamCorrectionBackend.Application.Features.ExamTasks.Queries.GetAllExamTasksFromExam;
using ExamCorrectionBackend.Application.Features.StudentSolutions.Commands.CreateStudentSolution;
using ExamCorrectionBackend.Domain.Entities;
using ExcelDataReader;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamCorrectionBackend.Controllers
{
    [EnableCors]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DatasetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;

        public DatasetController(IMediator mediator, IHttpClientFactory httpClientFactory, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _httpClientFactory = httpClientFactory;
            _environment = environment;
        }

        // GET: api/<DatasetController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatasetDto>>> Get()
        {
            var request = new GetAllDatasetRequest();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // GET api/<DatasetController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DatasetDto>> Get(int id)
        {
            var request = new GetDatasetRequest(){DatasetId = id};
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        // POST api/<DatasetController>
        [HttpPost]
        public async Task<ActionResult<DatasetDto>> Post([FromBody] DatasetDto dto)
        {
            var request = new CreateDatasetRequest() {DatasetDto = dto};
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<DatasetDto>) Ok(result) : BadRequest();
        }

        // PUT api/<DatasetController>
        [HttpPut]
        public async Task<ActionResult<DatasetDto>> Put([FromBody] DatasetDto dto)
        {
            var request = new UpdateDatasetRequest() { DatasetDto = dto };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<DatasetDto>) Ok(result) : BadRequest();
        }

        // DELETE api/<DatasetController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DatasetDto>> Delete(int id)
        {
            var request = new DeleteDatasetRequest() { DatasetId = id };
            var result = await _mediator.Send(request);
            return result != null ? (ActionResult<DatasetDto>) Ok(result) : BadRequest();
        }

        [HttpPost("Excel")]
        public async Task<ActionResult<IEnumerable<DatasetDto>>> UploadExcel([FromServices] IHostingEnvironment hostingEnvironment)
        {
            try
            {
                var datasetDtos = new List<DatasetDto>();

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
                    

                    var dataset =
                        this.GetDataset(_environment.WebRootPath + "\\Upload\\" + file.FileName);

                    foreach (var data in dataset)
                    {
                        var request = new CreateDatasetRequest() { DatasetDto = data };
                        var result = await _mediator.Send(request);
                        datasetDtos.Add(result);
                    }
                }

                return Ok(datasetDtos);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<DatasetDto> GetDataset(string fileName)
        {
            var dtos = new List<DatasetDto>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            while (reader.Read())
            {
                dtos.Add(new DatasetDto()
                {
                    Sentence1 = reader.GetValue(0).ToString(),
                    Sentence2 = reader.GetValue(1).ToString(),
                    Score = Decimal.Parse(reader.GetValue(2).ToString() ?? throw new InvalidOperationException(), NumberStyles.AllowDecimalPoint, new CultureInfo("en-US")),
                    IsSimilar = Boolean.Parse((ReadOnlySpan<char>) reader.GetValue(3).ToString())
                });
            }

            return dtos;
        }
    }
}
