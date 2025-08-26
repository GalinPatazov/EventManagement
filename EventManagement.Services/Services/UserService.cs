using EventManagement.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Services.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IValidator<User> _validator;

        public UserService(IUserRepository userRepo, IValidator<User> validator)
        {
            _userRepo = userRepo;
            _validator = validator;
        }

        
        public async Task<List<User>> GetAllAsync() => await _userRepo.GetAll();
        public async Task<User> GetByIdAsync(int id) => await _userRepo.Get(id);

        public async Task<User> AddAsync(User model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _userRepo.Create(model);
        }

        public async Task<User> UpdateAsync(User model)
        {
            ValidationResult result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await _userRepo.Update(model);
        }

        public async Task DeleteAsync(int id) => await _userRepo.Delete(id);

        
        public async Task<List<User>> GetUsersByEvent(int eventId) =>
            await _userRepo.GetUsersByEventId(eventId);

        public async Task<List<User>> GetUsersByRegistrationStatus(bool isRegistered) =>
            await _userRepo.GetUsersByRegistrationStatus(isRegistered);
    }
}
