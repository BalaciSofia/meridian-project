using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly AppDbContext _context;

        public EventsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsForUser(int userId)
        {
            return await _context.Events
                .Include(e => e.CreatedByAccount)
                .Where(e => e.CreatedByAccountId == userId
                    || _context.EventParticipants.Any(ep => ep.EventId == e.Id && ep.AccountId == userId))
                .ToListAsync();
        }

        public async Task AddEvent(Event e)
        {
            await _context.Events.AddAsync(e);
            await _context.SaveChangesAsync();
        }

        public async Task AddParticipantToEvent(EventParticipant eventParticipant)
        {
            await _context.EventParticipants.AddAsync(eventParticipant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusParticipantEvent(EventParticipant eventParticipant)
        {
            _context.EventParticipants.Update(eventParticipant);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventParticipant>> GetParticipantsForEvent(int eventId)
        {
            return await _context.EventParticipants
                .Include(ep => ep.Account)
                .Where(ep => ep.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Event?> GetEventById(int eventId)
        {
            return await _context.Events
                .Include(e => e.CreatedByAccount)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }
    }
}
