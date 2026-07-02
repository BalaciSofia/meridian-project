using backend.DTOs;
using backend.Mapping;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly EventMapper _eventMapper;

        public EventsController(IEventsService eventsService, EventMapper eventMapper)
        {
            _eventsService = eventsService;
            _eventMapper = eventMapper;
        }

        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<EventResponse>>> GetMyEvents()
        {
            var accountId = GetCurrentAccountId();
            var events = await _eventsService.GetAllEventsForUser(accountId);
            var response = _eventMapper.ToResponses(events);

            return Ok(response);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventResponse>> GetEventById(int eventId)
        {
            var eventEntity = await _eventsService.GetEventById(eventId);

            if (eventEntity == null)
            {
                return NotFound();
            }

            var response = _eventMapper.ToResponse(eventEntity);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(CreateEventRequest request)
        {
            var accountId = GetCurrentAccountId();
            var eventEntity = _eventMapper.ToEntity(request);
            eventEntity.CreatedByAccountId = accountId;

            await _eventsService.AddEvent(eventEntity, request.ParticipantIds);
            return CreatedAtAction(nameof(GetEventById), new { eventId = eventEntity.Id }, null);
        }

        [HttpGet("{eventId}/participants")]
        public async Task<ActionResult<IEnumerable<EventParticipantResponse>>> GetParticipantsForEvent(int eventId)
        {
            var participants = await _eventsService.GetParticipantsForEvent(eventId);
            var response = _eventMapper.ToParticipantResponses(participants);

            return Ok(response);
        }

        [HttpPut("participants/{participantId}")]
        public async Task<IActionResult> UpdateStatusParticipantEvent(
            int participantId,
            UpdateEventParticipantStatusRequest request)
        {
            if (participantId != request.Id)
            {
                return BadRequest();
            }

            var eventParticipant = _eventMapper.ToEntity(request);

            await _eventsService.UpdateStatusParticipantEvent(eventParticipant);
            return CreatedAtAction(nameof(GetParticipantsForEvent), new { eventId = request.EventId }, null);
        }

        private int GetCurrentAccountId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
