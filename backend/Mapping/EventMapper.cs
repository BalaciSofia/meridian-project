using backend.DTOs;
using backend.Models;
using Riok.Mapperly.Abstractions;

namespace backend.Mapping
{
    [Mapper]
    public partial class EventMapper
    {
        [MapperIgnoreTarget(nameof(Event.Id))]
        [MapperIgnoreTarget(nameof(Event.CreatedByAccountId))]
        [MapperIgnoreTarget(nameof(Event.CreatedByAccount))]
        [MapperIgnoreSource(nameof(CreateEventRequest.ParticipantIds))]
        public partial Event ToEntity(CreateEventRequest request);

        [MapperIgnoreTarget(nameof(EventParticipant.Event))]
        [MapperIgnoreTarget(nameof(EventParticipant.Account))]
        public partial EventParticipant ToEntity(UpdateEventParticipantStatusRequest request);

        [MapPropertyFromSource(nameof(EventResponse.CreatedByName), Use = nameof(MapCreatedByName))]
        public partial EventResponse ToResponse(Event eventEntity);

        public partial IEnumerable<EventResponse> ToResponses(IEnumerable<Event> events);

        [MapPropertyFromSource(nameof(EventParticipantResponse.ParticipantName), Use = nameof(MapParticipantName))]
        [MapperIgnoreSource(nameof(EventParticipant.Event))]
        public partial EventParticipantResponse ToResponse(EventParticipant eventParticipant);

        public partial IEnumerable<EventParticipantResponse> ToParticipantResponses(IEnumerable<EventParticipant> eventParticipants);

        private static string MapCreatedByName(Event eventEntity)
        {
            return $"{eventEntity.CreatedByAccount.FirstName} {eventEntity.CreatedByAccount.LastName}".Trim();
        }

        private static string MapParticipantName(EventParticipant eventParticipant)
        {
            return $"{eventParticipant.Account.FirstName} {eventParticipant.Account.LastName}".Trim();
        }
    }
}
