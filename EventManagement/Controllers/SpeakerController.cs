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
    public class SpeakerController : ControllerBase
    {
        private readonly SpeakerService _speakerService;
        private readonly IMapper _mapper;

        public SpeakerController(SpeakerService speakerService, IMapper mapper)
        {
            _speakerService = speakerService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var speakers = await _speakerService.GetAllAsync();
            var dtos = _mapper.Map<List<SpeakerDTO>>(speakers);
            return Ok(dtos);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var speaker = await _speakerService.GetByIdAsync(id);
            if (speaker == null) return NotFound();

            var dto = _mapper.Map<SpeakerDTO>(speaker);
            return Ok(dto);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(SpeakerDTO model)
        {
            try
            {
                var entity = _mapper.Map<Speaker>(model);
                var created = await _speakerService.AddAsync(entity);
                var dto = _mapper.Map<SpeakerDTO>(created);

                return CreatedAtAction(nameof(Get), new { id = dto.SpeakerID }, dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SpeakerDTO model)
        {
            if (id != model.SpeakerID) return BadRequest("Speaker ID mismatch");

            try
            {
                var entity = _mapper.Map<Speaker>(model);
                var updated = await _speakerService.UpdateAsync(entity);
                var dto = _mapper.Map<SpeakerDTO>(updated);

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
            await _speakerService.DeleteAsync(id);
            return NoContent();
        }

        
        [HttpGet("by-event/{eventId}")]
        public async Task<IActionResult> GetSpeakersByEvent(int eventId)
        {
            var speakers = await _speakerService.GetSpeakersByEvent(eventId);
            var dtos = _mapper.Map<List<SpeakerDTO>>(speakers);
            return Ok(dtos);
        }
    }   
}
