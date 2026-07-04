export interface EventResponse {
  id: number;
  title: string;
  startingTime: string;
  endingTime: string;
  description: string;
  eventLink: string | null;
  location: string | null;
  createdByAccountId: number;
  createdByName: string;
}

export interface CreateEventRequest {
  title: string;
  startingTime: string;
  endingTime: string;
  description: string;
  eventLink: string | null;
  location: string | null;
  participantIds: number[];
}

export interface EventParticipantResponse {
  id: number;
  eventId: number;
  accountId: number;
  participantName: string;
  status: string;
}

export interface UpdateEventParticipantStatusRequest {
  id: number;
  eventId: number;
  accountId: number;
  status: string;
}

export interface WorkScheduleResponse {
  id: number;
  accountId: number;
  date: string;
  workMode: string;
}

export interface SetWorkScheduleRequest {
  date: string;
  workMode: string;
}
