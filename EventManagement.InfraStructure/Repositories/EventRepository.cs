using EventManagement.Core.Models;
using EventManagement.Core.Repositories;
using EventManagement.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.InfraStructure.Repositories
{
    public class EventRepository : IEventManagementRepository
    {
        private readonly EventDbContext _context;

        public EventRepository(EventDbContext context)
        {
            _context = context;
        }

        // CRUD операции

        public async Task<Event> Create(Event model)
        {
            await _context.Events.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Event> Get(int id)
        {
            return await _context.Events
                .Include(e => e.Registrations)       // зарежда регистрациите
                .ThenInclude(r => r.User)            // и потребителите
                .FirstOrDefaultAsync(e => e.EventId == id);
        }

        public async Task<List<Event>> GetAll()
        {
            return await _context.Events
                .Include(e => e.Speaker)             // зарежда спийкъра
                .ToListAsync();
        }

        public async Task<Event> Update(Event model)
        {
            _context.Events.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Events.FindAsync(id);
            if (entity != null)
            {
                _context.Events.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Complex Queries

        // 1️⃣ Всички събития на даден потребител (с филтър за предстоящи или минали)
        public async Task<List<Event>> GetEventsByUser(int userId, bool upcomingOnly)
        {
            var query = _context.Registrations
                .Where(r => r.UserId == userId)
                .Select(r => r.Event)
                .Include(e => e.Speaker)
                .AsQueryable();

            if (upcomingOnly)
                query = query.Where(e => e.Date > DateTime.Now);
            else
                query = query.Where(e => e.Date <= DateTime.Now);

            return await query.ToListAsync();
        }

        // 2️⃣ Всички потребители за дадено събитие
        public async Task<List<User>> GetUsersByEvent(int eventId)
        {
            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .Select(r => r.User)
                .ToListAsync();
        }

        // 3️⃣ Предстоящи събития със спийкъра им
        public async Task<List<Event>> GetUpcomingEventsWithSpeakers()
        {
            return await _context.Events
                .Where(e => e.Date > DateTime.Now)
                .Include(e => e.Speaker)
                .ToListAsync();
        }
    }
}
