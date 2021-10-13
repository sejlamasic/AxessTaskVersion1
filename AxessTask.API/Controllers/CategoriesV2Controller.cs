using AxessTask.API.Configuration;
using AxessTask.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AxessTask.API.Controllers
{
    /// <summary>
    /// Categories controller with IHttpClientFactory implementation
    /// </summary>
    [Route("api/[controller]/v2")]
    [ApiController]
    public class CategoriesV2Controller : ControllerBase
    {
        private readonly IOptions<AppSettings> _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriesV2Controller(
            IOptions<AppSettings> options,
            IHttpClientFactory httpClientFactory
        )
        {
            _options = options;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var client = _httpClientFactory.CreateClient();
            using var response = await client.GetAsync(_options.Value.NorthwindAPI + "/categories");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<NorthwindCategory>>(responseJson);
            return Ok(data);
        }
    }
}
