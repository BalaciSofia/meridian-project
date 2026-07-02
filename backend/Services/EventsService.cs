using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _eventsRepository;

        public EventsService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsForUser(int userId)
        {
            return await _eventsRepository.GetAllEventsForUser(userId);
        }

        public async Task AddEvent(Event e, IEnumerable<int> participantsIds)
        {
            await _eventsRepository.AddEvent(e);

            foreach (int id in participantsIds)
            {
                EventParticipant ep = new EventParticipant
                {
                    EventId = e.Id,
                    AccountId = id,
                    Status = "pending"
                };
                await _eventsRepository.AddParticipantToEvent(ep);
            }
            await _eventsRepository.AddParticipantToEvent(new EventParticipant
            {
                EventId = e.Id,
                AccountId = e.CreatedByAccountId,
                Status = "accepted"
            });
        }

        public async Task AddParticipantToEvent(EventParticipant eventParticipant)
        {
            await _eventsRepository.AddParticipantToEvent(eventParticipant);
        }

        public async Task UpdateStatusParticipantEvent(EventParticipant eventParticipant)
        {
            await _eventsRepository.UpdateStatusParticipantEvent(eventParticipant);
        }

        public async Task<IEnumerable<EventParticipant>> GetParticipantsForEvent(int eventId)
        {
            return await _eventsRepository.GetParticipantsForEvent(eventId);
        }

        public async Task<Event?> GetEventById(int eventId)
        {
            return await _eventsRepository.GetEventById(eventId);
        }
    }
}
