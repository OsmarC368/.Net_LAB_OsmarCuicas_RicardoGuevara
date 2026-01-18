using Core.Entities;
using Core.Interfaces.Services;
using Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DTOs;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<User>>>> Get()
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
        public async Task<ActionResult<Response<User>>> Get(int id)
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

        /*[HttpPost]
        public async Task<ActionResult<Response<User>>> Post([FromBody] User user)
        {
            try
            {
                var response = await _service.Create(user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }*/

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<User>>> Put(int id, [FromBody] User user)
        {
            try
            {
                var response = await _service.Update(id, user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
		/// Método para eliminar a un usuario
		/// </summary>
		/// <returns>Respuesta con mensaje de éxito de eliminado y datos null</returns>
		[HttpDelete("{id}")]
		public async Task<ActionResult<Response<User>>> Delete(int id)
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

        [HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto user)
		{
			try
			{
				var Response = await _service.Login(user.Email, user.Password);

				return Ok(Response);
			}
			catch(Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}			
		}

        [HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterResponse registerResponse)
		{
			try
			{
				var Response = await _service.Register(registerResponse);

				return Ok(Response);
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}
    }
}
