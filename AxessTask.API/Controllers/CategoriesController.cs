using AxessTask.API.Configuration;
using AxessTask.API.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AxessTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IOptions<AppSettings> _options;

        public CategoriesController(
            IOptions<AppSettings> options
        )
        {
            _options = options;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var client = new HttpClient();
            using var response = await client.GetAsync(_options.Value.NorthwindAPI + "/categories");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<NorthwindCategory>>(responseJson);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = new HttpClient();
            using var response = await client.GetAsync(_options.Value.NorthwindAPI + $"/categories/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NorthwindCategory>(responseJson);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(NorthwindCategory category)
        {
            var client = new HttpClient();
            using var response = await client.PostAsync(_options.Value.NorthwindAPI + "/categories", 
                new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NorthwindCategory>(responseJson);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(NorthwindCategory category)
        {
            var client = new HttpClient();
            using var response = await client.PutAsync(_options.Value.NorthwindAPI + $"/categories/{category.Id}",
                new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json"));
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(responseJson));
            }

            var data = JsonConvert.DeserializeObject<NorthwindCategory>(responseJson);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatev2([FromRoute] int id, [FromBody] NorthwindCategory category)
        {
            category.Id = id;
            return await Update(category);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var client = new HttpClient();
            using var response = await client.DeleteAsync(_options.Value.NorthwindAPI + $"/categories/{id}");
             
            var responseJson = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(responseJson));
            }

            return Ok();
        }
    }
}
