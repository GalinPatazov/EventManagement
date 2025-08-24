using EventManagement.InfraStructure;
using EventManagement.Services.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationService _regService;

        public RegistrationController(RegistrationService regService)
        {
            _regService = regService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _regService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reg = await _regService.GetByIdAsync(id);
            return reg == null ? NotFound() : Ok(reg);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Registration model)
        {
            try
            {
                var created = await _regService.AddAsync(model);
                return CreatedAtAction(nameof(Get), new { id = created.RegistrationId }, created);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Registration model)
        {
            if (id != model.RegistrationId) return BadRequest("Registration ID mismatch");

            try
            {
                var updated = await _regService.UpdateAsync(model);
                return Ok(updated);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _regService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/registration/by-event/{eventId}
        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId) =>
            Ok(await _regService.GetByEvent(eventId));

        // GET: api/registration/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) =>
            Ok(await _regService.GetByUser(userId));

        // GET: api/registration/by-status?isRegistered=true
        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatus(bool isRegistered) =>
            Ok(await _regService.GetByStatus(isRegistered));
    }
}
