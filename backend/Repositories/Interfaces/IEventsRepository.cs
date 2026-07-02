using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IEventsRepository
    {
        Task AddEvent(Event e);
        Task AddParticipantToEvent(EventParticipant eventParticipant);
        Task<IEnumerable<Event>> GetAllEventsForUser(int userId);
        Task<Event?> GetEventById(int eventId);
        Task<IEnumerable<EventParticipant>> GetParticipantsForEvent(int eventId);
        Task UpdateStatusParticipantEvent(EventParticipant eventParticipant);
    }
}