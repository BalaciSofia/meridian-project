import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { catchError, forkJoin, of } from 'rxjs';
import { AuthService } from '../../core/auth/auth.service';
import { CalendarService } from '../../core/calendar/calendar.service';
import {
  EventParticipantResponse,
  EventResponse,
  WorkScheduleResponse,
} from '../../core/calendar/calendar.models';
import { EmployeeResponse } from '../../core/employees/employee.models';
import { EmployeeService } from '../../core/employees/employee.service';

interface CalendarDay {
  date: Date | null;
  dateKey: string;
  dayNumber: number;
  isToday: boolean;
  isSelected: boolean;
}

@Component({
  selector: 'app-calendar',
  imports: [FormsModule],
  templateUrl: './calendar.html',
  styleUrl: './calendar.scss',
})
export class Calendar implements OnInit {
  events = signal<EventResponse[]>([]);
  eventParticipants = signal<Record<number, EventParticipantResponse[]>>({});
  workSchedules = signal<WorkScheduleResponse[]>([]);
  colleagues = signal<EmployeeResponse[]>([]);
  monthDate = signal(this.startOfMonth(new Date()));
  selectedDate = signal(this.toDateKey(new Date()));
  errorMessage = signal('');
  successMessage = signal('');
  isLoading = signal(true);
  isSavingSchedule = signal(false);
  isEventModalOpen = signal(false);
  isAddingEvent = signal(false);
  updatingParticipantId = signal<number | null>(null);
  selectedParticipantIds = signal<number[]>([]);

  eventForm = {
    title: '',
    startTime: '09:00',
    endTime: '10:00',
    description: '',
    eventLink: '',
    location: '',
  };

  calendarDays = computed(() => this.buildCalendarDays(this.monthDate()));
  selectedDateLabel = computed(() => this.formatDateLabel(this.selectedDate()));
  monthLabel = computed(() =>
    this.monthDate().toLocaleDateString('en-US', { month: 'long', year: 'numeric' }),
  );

  constructor(
    private calendarService: CalendarService,
    private employeeService: EmployeeService,
    private authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.loadCalendarData();
    this.loadColleagues();
  }

  previousMonth(): void {
    const current = this.monthDate();
    this.monthDate.set(new Date(current.getFullYear(), current.getMonth() - 1, 1));
    this.loadWorkSchedules();
  }

  nextMonth(): void {
    const current = this.monthDate();
    this.monthDate.set(new Date(current.getFullYear(), current.getMonth() + 1, 1));
    this.loadWorkSchedules();
  }

  selectDate(day: CalendarDay): void {
    if (!day.date) {
      return;
    }

    this.selectedDate.set(day.dateKey);
  }

  getEventsForDate(dateKey: string): EventResponse[] {
    return this.events().filter((event) => this.toDateKey(new Date(event.startingTime)) === dateKey);
  }

  getScheduleForDate(dateKey: string): WorkScheduleResponse | undefined {
    return this.workSchedules().find((schedule) => this.toDateKey(new Date(schedule.date)) === dateKey);
  }

  setWorkMode(workMode: string): void {
    this.clearMessages();
    this.isSavingSchedule.set(true);

    this.calendarService
      .setMyWorkSchedule({
        date: `${this.selectedDate()}T00:00:00`,
        workMode,
      })
      .subscribe({
        next: () => {
          this.successMessage.set('Work schedule updated.');
          this.isSavingSchedule.set(false);
          this.loadWorkSchedules();
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage.set(this.getErrorMessage(error, 'Could not update work schedule.'));
          this.isSavingSchedule.set(false);
        },
      });
  }

  openEventModal(): void {
    this.clearMessages();
    this.eventForm = {
      title: '',
      startTime: '09:00',
      endTime: '10:00',
      description: '',
      eventLink: '',
      location: '',
    };
    this.selectedParticipantIds.set([]);
    this.isEventModalOpen.set(true);
  }

  closeEventModal(): void {
    this.isEventModalOpen.set(false);
  }

  addEvent(): void {
    this.clearMessages();

    if (!this.eventForm.title.trim()) {
      this.errorMessage.set('Title is required.');
      return;
    }

    this.isAddingEvent.set(true);

    this.calendarService
      .addEvent({
        title: this.eventForm.title,
        startingTime: `${this.selectedDate()}T${this.eventForm.startTime}:00`,
        endingTime: `${this.selectedDate()}T${this.eventForm.endTime}:00`,
        description: this.eventForm.description,
        eventLink: this.eventForm.eventLink || null,
        location: this.eventForm.location || null,
        participantIds: this.selectedParticipantIds(),
      })
      .subscribe({
        next: () => {
          this.successMessage.set('Event added.');
          this.isAddingEvent.set(false);
          this.isEventModalOpen.set(false);
          this.loadEvents();
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage.set(this.getErrorMessage(error, 'Could not add event.'));
          this.isAddingEvent.set(false);
        },
      });
  }

  toggleParticipant(accountId: number, event: Event): void {
    const checked = (event.target as HTMLInputElement).checked;
    const currentIds = this.selectedParticipantIds();

    if (checked) {
      this.selectedParticipantIds.set([...currentIds, accountId]);
      return;
    }

    this.selectedParticipantIds.set(currentIds.filter((id) => id !== accountId));
  }

  isParticipantSelected(accountId: number): boolean {
    return this.selectedParticipantIds().includes(accountId);
  }

  formatEventTime(event: EventResponse): string {
    return `${this.formatTime(event.startingTime)} - ${this.formatTime(event.endingTime)}`;
  }

  getCurrentUserParticipant(eventId: number): EventParticipantResponse | undefined {
    const currentUserId = this.authService.currentUser()?.accountId;

    return this.eventParticipants()[eventId]?.find((participant) => participant.accountId === currentUserId);
  }

  updateMyEventStatus(event: EventResponse, status: string): void {
    const participant = this.getCurrentUserParticipant(event.id);

    if (!participant) {
      this.errorMessage.set('Could not find your invitation for this event.');
      return;
    }

    this.clearMessages();
    this.updatingParticipantId.set(participant.id);

    this.calendarService
      .updateParticipantStatus({
        id: participant.id,
        eventId: participant.eventId,
        accountId: participant.accountId,
        status,
      })
      .subscribe({
        next: () => {
          this.successMessage.set('Event response updated.');
          this.updatingParticipantId.set(null);
          this.loadEventParticipants(this.events());
        },
        error: (error: HttpErrorResponse) => {
          this.errorMessage.set(this.getErrorMessage(error, 'Could not update event response.'));
          this.updatingParticipantId.set(null);
        },
      });
  }

  private loadCalendarData(): void {
    this.isLoading.set(true);
    this.loadEvents();
    this.loadWorkSchedules();
  }

  private loadEvents(): void {
    this.calendarService.getMyEvents().subscribe({
      next: (events) => {
        this.events.set(events);
        this.loadEventParticipants(events);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load events.');
        this.isLoading.set(false);
      },
    });
  }

  private loadEventParticipants(events: EventResponse[]): void {
    if (events.length === 0) {
      this.eventParticipants.set({});
      return;
    }

    forkJoin(
      events.map((event) =>
        this.calendarService.getEventParticipants(event.id).pipe(
          // If one participants request fails, keep the calendar usable.
          catchError(() => of([] as EventParticipantResponse[])),
        ),
      ),
    ).subscribe((participantsByEvent) => {
      const participantsMap: Record<number, EventParticipantResponse[]> = {};

      events.forEach((event, index) => {
        participantsMap[event.id] = participantsByEvent[index];
      });

      this.eventParticipants.set(participantsMap);
    });
  }

  private loadWorkSchedules(): void {
    const firstDay = this.monthDate();
    const lastDay = new Date(firstDay.getFullYear(), firstDay.getMonth() + 1, 0);

    this.calendarService
      .getMyWorkSchedules(`${this.toDateKey(firstDay)}T00:00:00`, `${this.toDateKey(lastDay)}T23:59:59`)
      .subscribe({
        next: (workSchedules) => this.workSchedules.set(workSchedules),
        error: () => this.errorMessage.set('Could not load work schedule.'),
      });
  }

  private loadColleagues(): void {
    const currentUserId = this.authService.currentUser()?.accountId;

    this.employeeService.getColleagues().subscribe({
      next: (colleagues) => {
        this.colleagues.set(colleagues.filter((colleague) => colleague.id !== currentUserId));
      },
      error: () => this.errorMessage.set('Could not load colleagues.'),
    });
  }

  private buildCalendarDays(monthDate: Date): CalendarDay[] {
    const days: CalendarDay[] = [];
    const firstDay = this.startOfMonth(monthDate);
    const lastDay = new Date(firstDay.getFullYear(), firstDay.getMonth() + 1, 0);
    const firstWeekday = firstDay.getDay();
    const todayKey = this.toDateKey(new Date());

    for (let i = 0; i < firstWeekday; i++) {
      days.push({
        date: null,
        dateKey: `empty-${i}`,
        dayNumber: 0,
        isToday: false,
        isSelected: false,
      });
    }

    for (let dayNumber = 1; dayNumber <= lastDay.getDate(); dayNumber++) {
      const date = new Date(firstDay.getFullYear(), firstDay.getMonth(), dayNumber);
      const dateKey = this.toDateKey(date);

      days.push({
        date,
        dateKey,
        dayNumber,
        isToday: dateKey === todayKey,
        isSelected: dateKey === this.selectedDate(),
      });
    }

    return days;
  }

  private startOfMonth(date: Date): Date {
    return new Date(date.getFullYear(), date.getMonth(), 1);
  }

  private toDateKey(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  private formatDateLabel(dateKey: string): string {
    return new Date(`${dateKey}T00:00:00`).toLocaleDateString('en-US', {
      weekday: 'long',
      month: 'long',
      day: 'numeric',
    });
  }

  private formatTime(dateValue: string): string {
    return new Date(dateValue).toLocaleTimeString('en-US', {
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  private clearMessages(): void {
    this.errorMessage.set('');
    this.successMessage.set('');
  }

  private getErrorMessage(error: HttpErrorResponse, fallback: string): string {
    if (typeof error.error === 'string' && error.error.trim()) {
      return error.error;
    }

    return fallback;
  }
}
