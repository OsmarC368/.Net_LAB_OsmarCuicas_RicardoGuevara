using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")] 
    public class StepUserController : ControllerBase
    {
        private readonly IStepUserService _service;

        public StepUserController(IStepUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<StepUser>>>>  Get()
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
        public async Task<ActionResult<Response<StepUser>>> GetById(int id)
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
        public async Task<ActionResult<Response<StepUser>>> Post([FromBody] StepUser stepUser)
        {
            try
            {
                var response = await _service.Create(stepUser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Response<StepUser>>> Update(int id, [FromBody] StepUser stepUser)
        {
            try
            {
                var response = await _service.Update(id, stepUser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<StepUser>>> Delete(int id)
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