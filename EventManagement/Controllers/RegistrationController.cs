using AutoMapper;
using EventManagement.Core.DTOs;
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
        private readonly IMapper _mapper;

        public RegistrationController(RegistrationService regService, IMapper mapper)
        {
            _regService = regService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regs = await _regService.GetAllAsync();
            var dto = _mapper.Map<List<RegistrationDTO>>(regs);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reg = await _regService.GetByIdAsync(id);
            if (reg == null) return NotFound();

            var dto = _mapper.Map<RegistrationDTO>(reg);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationDTO model)
        {
            try
            {
                var entity = _mapper.Map<Registration>(model);
                var created = await _regService.AddAsync(entity);
                var dto = _mapper.Map<RegistrationDTO>(created);

                return CreatedAtAction(nameof(Get), new { id = dto.RegistrationId }, dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RegistrationDTO model)
        {
            if (id != model.RegistrationId) return BadRequest("Registration ID mismatch");

            try
            {
                var entity = _mapper.Map<Registration>(model);
                var updated = await _regService.UpdateAsync(entity);
                var dto = _mapper.Map<RegistrationDTO>(updated);

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
            await _regService.DeleteAsync(id);
            return NoContent();
        }

        // GET: api/registration/by-event/{eventId}
        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var regs = await _regService.GetByEvent(eventId);
            var dto = _mapper.Map<List<RegistrationDTO>>(regs);
            return Ok(dto);
        }

        // GET: api/registration/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var regs = await _regService.GetByUser(userId);
            var dto = _mapper.Map<List<RegistrationDTO>>(regs);
            return Ok(dto);
        }

        // GET: api/registration/by-status?isRegistered=true
        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatus(bool isRegistered)
        {
            var regs = await _regService.GetByStatus(isRegistered);
            var dto = _mapper.Map<List<RegistrationDTO>>(regs);
            return Ok(dto);
        }
    }
}
