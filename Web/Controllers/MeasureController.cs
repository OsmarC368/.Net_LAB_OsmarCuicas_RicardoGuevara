using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly IMeasureService _measureService;

        public MeasureController(IMeasureService measureService)
        {
            _measureService = measureService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measure>>> Get()
        {
            var measures = await _measureService.GetAll();
            return Ok(measures);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Measure>> GetById(int id)
        {
            try
            {
                var measures = await _measureService.GetById(id);
                return Ok(measures);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Measure>> Post([FromBody] Measure measure)
        {
            try
            {
                var newMeasureCreated = await _measureService.Create(measure);
                return CreatedAtAction(nameof(GetById), new {id = newMeasureCreated.id}, newMeasureCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Measure>> Update(int id, [FromBody] Measure measure)
        {
            try
            {
                var updatedMeasure = await _measureService.Update(id, measure);
                return Ok(updatedMeasure);
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
                await _measureService.Delete(id);
                return Ok("Measure Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}