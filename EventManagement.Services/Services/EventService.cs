using EventManagement.Core.Models;
using EventManagement.Core.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace EventManagement.Services.Services
{
    public class EventService
    {
        private readonly IEventManagementRepository _eventRepo;
        private readonly IValidator<Event> _validator;

        public EventService(IEventManagementRepository eventRepo, IValidator<Event> validator)
        {
            _eventRepo = eventRepo;
            _validator = validator;
        }

        // CRUD
        public async Task<List<Event>> GetAllAsync() => await _eventRepo.GetAll();
        public async Task<Event> GetByIdAsync(int id) => await _eventRepo.Get(id);

        public async Task<Event> AddAsync(Event model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _eventRepo.Create(model);
        }

        public async Task<Event> UpdateAsync(Event model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _eventRepo.Update(model);
        }

        public async Task DeleteAsync(int id) => await _eventRepo.Delete(id);

        // Complex queries
        public async Task<List<Event>> GetEventsByUser(int userId, bool upcomingOnly) =>
            await _eventRepo.GetEventsByUser(userId, upcomingOnly);

        public async Task<List<User>> GetUsersByEvent(int eventId) =>
            await _eventRepo.GetUsersByEvent(eventId);

        public async Task<List<Event>> GetUpcomingEventsWithSpeakers() =>
            await _eventRepo.GetUpcomingEventsWithSpeakers();
    }
}
