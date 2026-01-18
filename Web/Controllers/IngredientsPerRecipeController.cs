using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Responses;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsPerRecipeController : ControllerBase
    {
        private readonly IIngredientsPerRecipeService _service;

        public IngredientsPerRecipeController(IIngredientsPerRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<IngredientsPerRecipe>>>>  Get()
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
        public async Task<ActionResult<Response<IngredientsPerRecipe>>> GetById(int id)
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
        public async Task<ActionResult<Response<IngredientsPerRecipe>>> Post([FromBody] IngredientsPerRecipe ingredientsPerRecipe)
        {
            try
            {
                var response = await _service.Create(ingredientsPerRecipe);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<IngredientsPerRecipe>>> Update(int id, [FromBody] IngredientsPerRecipe ingredientsPerRecipe)
        {
            try
            {
                var response = await _service.Update(id, ingredientsPerRecipe);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<IngredientsPerRecipe>>> Delete(int id)
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