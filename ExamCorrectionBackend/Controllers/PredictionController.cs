using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExamCorrectionBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ExamCorrectionBackend.Controllers
{
    [EnableCors]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PredictionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ActionResult<decimal>> Predict([FromBody] ScoreRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonObject = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,
                "application/json");
            using var httpResponse = await client.PostAsync("http://127.0.0.1:5002/api/predict", jsonObject);
            httpResponse.EnsureSuccessStatusCode();
            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            var scoreResult = Decimal.Parse(responseBody, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"));
            return Ok(scoreResult);
        }
    }
}
