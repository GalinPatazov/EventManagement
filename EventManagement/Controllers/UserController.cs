using AutoMapper;
using EventManagement.Core.DTOs;
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
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var dtos = _mapper.Map<List<UserDTO>>(users);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var dto = _mapper.Map<UserDTO>(user);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDTO model)
        {
            try
            {
                var entity = _mapper.Map<User>(model);
                var created = await _userService.AddAsync(entity);
                var dto = _mapper.Map<UserDTO>(created);

                return CreatedAtAction(nameof(Get), new { id = dto.UserId }, dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDTO model)
        {
            if (id != model.UserId) return BadRequest("User ID mismatch");

            try
            {
                var entity = _mapper.Map<User>(model);
                var updated = await _userService.UpdateAsync(entity);
                var dto = _mapper.Map<UserDTO>(updated);

                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        
        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetUsersByEvent(int eventId)
        {
            var users = await _userService.GetUsersByEvent(eventId);
            var dtos = _mapper.Map<List<UserDTO>>(users);
            return Ok(dtos);
        }

        
        [HttpGet("by-status")]
        public async Task<IActionResult> GetUsersByStatus(bool isRegistered)
        {
            var users = await _userService.GetUsersByRegistrationStatus(isRegistered);
            var dtos = _mapper.Map<List<UserDTO>>(users);
            return Ok(dtos);
        }
    }
}
