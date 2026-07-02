using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IEventsService
    {
        Task AddEvent(Event e, IEnumerable<int> participantsIds);
        Task AddParticipantToEvent(EventParticipant eventParticipant);
        Task<IEnumerable<Event>> GetAllEventsForUser(int userId);
        Task<Event?> GetEventById(int eventId);
        Task<IEnumerable<EventParticipant>> GetParticipantsForEvent(int eventId);
        Task UpdateStatusParticipantEvent(EventParticipant eventParticipant);
    }
}