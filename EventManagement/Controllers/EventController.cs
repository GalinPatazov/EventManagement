using AutoMapper;
using EventManagement.Core.DTOs;
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
        private readonly IMapper _mapper;

        public EventController(EventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        // GET: api/event
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            var eventDtos = _mapper.Map<List<EventDTO>>(events);
            return Ok(eventDtos);
        }

        // GET: api/event/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return NotFound();

            var eventDto = _mapper.Map<EventDTO>(ev);
            return Ok(eventDto);
        }

        // POST: api/event
        [HttpPost]
        public async Task<IActionResult> Create(EventDTO model)
        {
            try
            {
                var entity = _mapper.Map<Event>(model);
                var created = await _eventService.AddAsync(entity);
                var createdDto = _mapper.Map<EventDTO>(created);

                return CreatedAtAction(nameof(Get), new { id = createdDto.EventId }, createdDto);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        // PUT: api/event/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EventDTO model)
        {
            if (id != model.EventId) return BadRequest("Event ID mismatch");

            try
            {
                var entity = _mapper.Map<Event>(model);
                var updated = await _eventService.UpdateAsync(entity);
                var updatedDto = _mapper.Map<EventDTO>(updated);

                return Ok(updatedDto);
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

        // GET: api/event/user/{userId}?upcomingOnly=true
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEventsByUser(int userId, bool upcomingOnly = true)
        {
            var events = await _eventService.GetEventsByUser(userId, upcomingOnly);
            var eventDtos = _mapper.Map<List<EventDTO>>(events);

            return Ok(eventDtos);
        }

        // GET: api/event/{eventId}/users
        [HttpGet("{eventId}/users")]
        public async Task<IActionResult> GetUsersByEvent(int eventId)
        {
            var users = await _eventService.GetUsersByEvent(eventId);
            var userDtos = _mapper.Map<List<UserDTO>>(users);

            return Ok(userDtos);
        }

        // GET: api/event/upcoming
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEventsWithSpeakers()
        {
            var events = await _eventService.GetUpcomingEventsWithSpeakers();
            var eventDtos = _mapper.Map<List<EventDTO>>(events);

            return Ok(eventDtos);
        }
    }
}
