# What I Would Do Next

With two more weeks, here is what I would build, in order.

## Priority 1 — Features that would fundamentally improve the experience

- **Tests.** There are no automated tests right now. I would add unit tests
  for the backend services, and some integration tests for the main API
  endpoints (login, documents, events).
- **Real validation.** Right now most forms only check "is this field
  filled in." I would add proper checks: valid emails, end time after start
  time, file size and type limits on uploads. FluentValidation is already a
  dependency, I just have not wired it up yet.
- **Notifications.** Right now you only find out about a new document
  status or an event invite by opening the app. A simple notification list
  would fix this.
- **Let colleagues see each other's calendar.** Right now you can only see
  your own schedule. Being able to check when a colleague is free and ask
  for time with them was part of my original plan.

## Priority 2 — Features that would add significant value

- **Document templates.** Let HR attach a template file to a document type.
- **Work mode vs event conflicts.** Warn or auto-decline an event if you
  mark yourself absent that day.
- **Department-level permissions.** Right now it is just HR/Admin vs
  everyone else. A team lead managing their own team would need more than
  that.
- **Move config out of the code.** The API address is hardcoded in the
  frontend. The backend's JWT key and connection string are plain text in a
  committed file. Fine for now, not fine long term.
- **Profile pictures.**

## Priority 3 — Nice-to-have improvements and why they matter

- **Only show announcements after your hire date.** Matters more once the
  feed has years of history, not yet at this size.
- **Search and filters on the employee list.** Not needed yet at 200
  people, would matter as the company grows.
- **Export your calendar** (`.ics` file), so it can show up in your own
  calendar app too.

