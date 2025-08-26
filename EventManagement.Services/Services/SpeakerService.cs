using EventManagement.Core.Repositories;
using EventManagement.InfraStructure;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Services.Services
{
    public class SpeakerService
    {
        private readonly ISpeakerRepository _speakerRepo;
        private readonly IValidator<Speaker> _validator;

        public SpeakerService(ISpeakerRepository speakerRepo, IValidator<Speaker> validator)
        {
            _speakerRepo = speakerRepo;
            _validator = validator;
        }

        
        public async Task<List<Speaker>> GetAllAsync() => await _speakerRepo.GetAll();
        public async Task<Speaker> GetByIdAsync(int id) => await _speakerRepo.Get(id);

        public async Task<Speaker> AddAsync(Speaker model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _speakerRepo.Create(model);
        }

        public async Task<Speaker> UpdateAsync(Speaker model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _speakerRepo.Update(model);
        }

        public async Task DeleteAsync(int id) => await _speakerRepo.Delete(id);

        
        public async Task<List<Speaker>> GetSpeakersByEvent(int eventId) =>
            await _speakerRepo.GetSpeakersByEventId(eventId);
    }
}
