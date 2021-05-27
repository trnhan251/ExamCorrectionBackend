using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class BlobController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlobController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/<BlobController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var objectId = _httpContextAccessor.HttpContext.User.GetObjectId();
            }

            return new string[] { "value1", "value2" };
        }

        // GET api/<BlobController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlobController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BlobController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlobController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
