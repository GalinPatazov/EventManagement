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
    public class RegistrationService
    {
        private readonly IRegistrationReposiory _regRepo;
        private readonly IValidator<Registration> _validator;

        public RegistrationService(IRegistrationReposiory regRepo, IValidator<Registration> validator)
        {
            _regRepo = regRepo;
            _validator = validator;
        }

        
        public async Task<List<Registration>> GetAllAsync() => await _regRepo.GetAll();
        public async Task<Registration> GetByIdAsync(int id) => await _regRepo.Get(id);

        public async Task<Registration> AddAsync(Registration model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _regRepo.Create(model);
        }

        public async Task<Registration> UpdateAsync(Registration model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _regRepo.Update(model);
        }

        public async Task DeleteAsync(int id) => await _regRepo.Delete(id);

        
        public async Task<List<Registration>> GetByEvent(int eventId) =>
            await _regRepo.GetRegistrationsByEventId(eventId);

        public async Task<List<Registration>> GetByUser(int userId) =>
            await _regRepo.GetRegistrationsByUserId(userId);

        public async Task<List<Registration>> GetByStatus(bool isRegistered) =>
            await _regRepo.GetRegistrationsByStatus(isRegistered);
    }
}
