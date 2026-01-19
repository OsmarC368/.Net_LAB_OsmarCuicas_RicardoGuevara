using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Responses;
using Core.Entities;
using Core.Interfaces.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.Auxiliar;
using System.Text.Json;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StepController : ControllerBase
    {
        private readonly IStepService _service;

        public StepController(IStepService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<Step>>>>  Get()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Step>>> GetById(int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id); 
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<Step>>> Post([FromBody] Step step)
        {
            try
            {
                var response = await _service.Create(step);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("image")]
        public async Task<ActionResult<Response<Step>>> Post([FromForm] CreateStepRequest request)
        {
            try
            {
                var imageUrl = string.Empty;
                using var httpClient = new HttpClient();
                using var formData = new MultipartFormDataContent();
                var stream = request.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ImageFile.ContentType ?? "image/jpeg");
                formData.Add(new StringContent("a265ee389c34416e946ea59c45925527"), "key");
                formData.Add(fileContent, "image", request.ImageFile.FileName ?? "receta.png");

                var apiImageResponse = await httpClient.PostAsync("https://api.imgbb.com/1/upload", formData);

                if (!apiImageResponse.IsSuccessStatusCode)
                {
                    return BadRequest(new { message = "Failed to upload image" });
                }

                var jsonApiImageResponse = await apiImageResponse.Content.ReadAsStringAsync();
                using(var doc = JsonDocument.Parse(jsonApiImageResponse))
                {
                    var root = doc.RootElement;
                    var data = root.GetProperty("data");
                    imageUrl = data.GetProperty("url").GetString() ?? string.Empty;
                }

                var step = new Step
                {
                    Name = request.Name,
                    Description = request.Description,
                    Duration = request.Duration,
                    imageURL = imageUrl
                };

                var response = await _service.Create(step);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Step>>> Update(int id, [FromBody] Step step)
        {
            try
            {
                var response = await _service.Update(id, step);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Step>>> Delete(int id)
        {
            try
			{
				var Response = await _service.Remove(id);

				return Ok(Response);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
        }
    }
}