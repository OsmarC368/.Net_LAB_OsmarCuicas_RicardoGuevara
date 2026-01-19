using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Responses;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Web.Auxiliar;
using System.Text.Json;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<Recipe>>>>  Get()
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
        public async Task<ActionResult<Response<Recipe>>> GetById(int id)
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
        public async Task<ActionResult<Response<Recipe>>> Post([FromBody] Recipe recipe)
        {
            try
            {
                var response = await _service.Create(recipe);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("image")]
        public async Task<ActionResult<Response<Recipe>>> Post([FromForm] CreateRecipeRequest request)
        {
            try
            {
                var imageUrl = string.Empty;
                using var httpClient = new HttpClient();
                using var formData = new MultipartFormDataContent();
                var stream = request.ImageFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ImageFile.ContentType ?? "image/jpeg");
                formData.Add(fileContent, "source", request.ImageFile.FileName);
                formData.Add(new StringContent("json"), "format");

                var apiImageResponse = await httpClient.PostAsync("https://freeimage.host/api/1/upload", formData);

                if (!apiImageResponse.IsSuccessStatusCode)
                {
                    return BadRequest(new { message = "Failed to upload image" });
                }

                var jsonApiImageResponse = await apiImageResponse.Content.ReadAsStringAsync();
                var responseDeserealized = JsonSerializer.Deserialize<FreeImageResponse>(jsonApiImageResponse);

                if (responseDeserealized?.Success == true)
                {
                    imageUrl = responseDeserealized.Image.Url;
                }
            
                var recipe = new Recipe
                {
                    Name = request.Name,
                    Description = request.Description,
                    Type = request.Type,
                    DifficultyLevel = request.DifficultyLevel,
                    Visibility = request.Visibility,
                    UserIdR = request.UserRID,
                    ImageUrl = imageUrl
                };

                var response = await _service.Create(recipe);
                return Ok(response);
                
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<Recipe>>> Update(int id, [FromBody] Recipe recipe)
        {
            try
            {
                var response = await _service.Update(id, recipe);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<Recipe>>> Delete(int id)
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