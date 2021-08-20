using System.Collections.Generic;
using System.Threading.Tasks;
using ExamCorrectionBackend.Application.Features.Courses.Queries.GetAllCourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ExamCorrectionBackend.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController
    {
        public HealthController()
        {
        }

        // GET: api/<CoursesController>
        [HttpGet]
        public string Get()
        {
            return "Server is running";
        }
    }
}