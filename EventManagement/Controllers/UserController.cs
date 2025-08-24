using EventManagement.Services;
using EventManagement.Services.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            try
            {
                var created = await _userService.AddAsync(model);
                return CreatedAtAction(nameof(Get), new { id = created.UserId }, created);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User model)
        {
            if (id != model.UserId) return BadRequest("User ID mismatch");

            try
            {
                var updated = await _userService.UpdateAsync(model);
                return Ok(updated);
            }
            catch (ValidationException ex) { return BadRequest(ex.Errors); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/user/by-event/{eventId}
        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetUsersByEvent(int eventId) =>
            Ok(await _userService.GetUsersByEvent(eventId));

        // GET: api/user/by-status?isRegistered=true
        [HttpGet("by-status")]
        public async Task<IActionResult> GetUsersByStatus(bool isRegistered) =>
            Ok(await _userService.GetUsersByRegistrationStatus(isRegistered));
    }
}
