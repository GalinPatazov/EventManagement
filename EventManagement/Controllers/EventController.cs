using EventManagement.Core.Models;
using EventManagement.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/event
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        // GET: api/event/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        // POST: api/event
        [HttpPost]
        public async Task<IActionResult> Create(Event model)
        {
            try
            {
                var created = await _eventService.AddAsync(model);
                return CreatedAtAction(nameof(Get), new { id = created.EventId }, created);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        // PUT: api/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Event model)
        {
            if (id != model.EventId) return BadRequest("Event ID mismatch");

            try
            {
                var updated = await _eventService.UpdateAsync(model);
                return Ok(updated);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        // DELETE: api/event/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteAsync(id);
            return NoContent();
        }

        // Complex queries

        // GET: api/event/user/{userId}?upcomingOnly=true
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEventsByUser(int userId, bool upcomingOnly = true)
        {
            var events = await _eventService.GetEventsByUser(userId, upcomingOnly);
            return Ok(events);
        }

        // GET: api/event/{eventId}/users
        [HttpGet("{eventId}/users")]
        public async Task<IActionResult> GetUsersByEvent(int eventId)
        {
            var users = await _eventService.GetUsersByEvent(eventId);
            return Ok(users);
        }

        // GET: api/event/upcoming
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEventsWithSpeakers()
        {
            var events = await _eventService.GetUpcomingEventsWithSpeakers();
            return Ok(events);
        }
    }
}
