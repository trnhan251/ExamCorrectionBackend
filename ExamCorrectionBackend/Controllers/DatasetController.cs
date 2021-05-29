using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Dto;
using ExamCorrectionBackend.Application.Features.Dataset.Commands.DeleteDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Commands.UpdateDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Queries.GetAllDataset;
using ExamCorrectionBackend.Application.Features.Dataset.Queries.GetDataset;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

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

        public DatasetController(IMediator mediator)
        {
            _mediator = mediator;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DatasetController>
        [HttpPut]
        public async Task<ActionResult<DatasetDto>> Put([FromBody] DatasetDto dto)
        {
            var request = new UpdateDatasetRequest() { DatasetDto = dto };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }

        // DELETE api/<DatasetController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DatasetDto>> Delete(int id)
        {
            var request = new DeleteDatasetRequest() { DatasetId = id };
            var result = await _mediator.Send(request);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
