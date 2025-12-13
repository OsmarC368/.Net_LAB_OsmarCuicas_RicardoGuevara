using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;

        public UserTypeController(IUserTypeService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> Get()
        {
            var userTypes = await _userTypeService.GetAll();
            return Ok(userTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetById(int id)
        {
            try
            {
                var userTypes = await _userTypeService.GetById(id);
                return Ok(userTypes);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserType>> Post([FromBody] UserType userType)
        {
            try
            {
                var newUserTypeCreated = await _userTypeService.Create(userType);
                return CreatedAtAction(nameof(GetById), new {id = newUserTypeCreated.id}, newUserTypeCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserType>> Update(int id, [FromBody] UserType userType)
        {
            try
            {
                var updatedUserType = await _userTypeService.Update(id, userType);
                return Ok(updatedUserType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userTypeService.Delete(id);
                return Ok("User Type Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}