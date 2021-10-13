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
    public class OrdersController : ControllerBase
    {
        private readonly IOptions<AppSettings> _options;

        public OrdersController(
            IOptions<AppSettings> options
        )
        {
            _options = options;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var client = new HttpClient();
            using var response = await client.GetAsync(_options.Value.NorthwindAPI + "/orders");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<NorthwindOrder>>(responseJson);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = new HttpClient();
            using var response = await client.GetAsync(_options.Value.NorthwindAPI + $"/orders/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NorthwindOrder>(responseJson);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(NorthwindOrder order)
        {
            var client = new HttpClient();
            using var response = await client.PostAsync(_options.Value.NorthwindAPI + "/orders",
                new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode);
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<NorthwindOrder>(responseJson);
            return Ok(data);
        }
        [HttpPut]
        public async Task<IActionResult> Update(NorthwindOrder order)
        {
            var client = new HttpClient();
            using var response = await client.PutAsync(_options.Value.NorthwindAPI + $"/orders/{order.id}",
                new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json"));
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(responseJson));
            }

            var data = JsonConvert.DeserializeObject<NorthwindOrder>(responseJson);
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatev2([FromRoute] int id, [FromBody] NorthwindOrder order)
        {
            order.id = id;
            return await Update(order);
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var client = new HttpClient();
            using var response = await client.DeleteAsync(_options.Value.NorthwindAPI + $"/orders/{id}");

            var responseJson = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, JsonConvert.DeserializeObject(responseJson));
            }

            return Ok();
        }
    }

}
