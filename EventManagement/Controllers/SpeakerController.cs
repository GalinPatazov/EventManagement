using EventManagement.InfraStructure;
using EventManagement.Services.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeakerController : ControllerBase
    {
        private readonly SpeakerService _speakerService;

        public SpeakerController(SpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _speakerService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var speaker = await _speakerService.GetByIdAsync(id);
            return speaker == null ? NotFound() : Ok(speaker);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Speaker model)
        {
            try
            {
                var created = await _speakerService.AddAsync(model);
                return CreatedAtAction(nameof(Get), new { id = created.SpeakerID }, created);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Speaker model)
        {
            if (id != model.SpeakerID) return BadRequest("Speaker ID mismatch");

            try
            {
                var updated = await _speakerService.UpdateAsync(model);
                return Ok(updated);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _speakerService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/speaker/by-event/{eventId}
        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetSpeakersByEvent(int eventId) =>
            Ok(await _speakerService.GetSpeakersByEvent(eventId));
    }
}
