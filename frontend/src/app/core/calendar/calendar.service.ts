import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CreateEventRequest,
  EventParticipantResponse,
  EventResponse,
  SetWorkScheduleRequest,
  UpdateEventParticipantStatusRequest,
  WorkScheduleResponse,
} from './calendar.models';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  private readonly eventsApiUrl = 'https://localhost:7166/api/events';
  private readonly workSchedulesApiUrl = 'https://localhost:7166/api/workSchedules';

  constructor(private http: HttpClient) {}

  getMyEvents(): Observable<EventResponse[]> {
    return this.http.get<EventResponse[]>(`${this.eventsApiUrl}/my`);
  }

  addEvent(request: CreateEventRequest): Observable<void> {
    return this.http.post<void>(this.eventsApiUrl, request);
  }

  getEventParticipants(eventId: number): Observable<EventParticipantResponse[]> {
    return this.http.get<EventParticipantResponse[]>(`${this.eventsApiUrl}/${eventId}/participants`);
  }

  updateParticipantStatus(request: UpdateEventParticipantStatusRequest): Observable<void> {
    return this.http.put<void>(`${this.eventsApiUrl}/participants/${request.id}`, request);
  }

  getMyWorkSchedules(from: string, to: string): Observable<WorkScheduleResponse[]> {
    const params = new HttpParams().set('from', from).set('to', to);

    return this.http.get<WorkScheduleResponse[]>(`${this.workSchedulesApiUrl}/my`, { params });
  }

  setMyWorkSchedule(request: SetWorkScheduleRequest): Observable<void> {
    return this.http.put<void>(`${this.workSchedulesApiUrl}/my`, request);
  }
}
