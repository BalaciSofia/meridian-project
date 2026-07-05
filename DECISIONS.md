# Decisions

## Product decisions

**Which features did you include?**

- Login, with the account created by HR beforehand and a forced password
  change on first login.
- A company announcement feed, with likes/dislikes and comments.
- A personal calendar: office/remote/absent per day, plus events you can
  accept or decline.
- A document checklist for onboarding paperwork: HR defines what documents
  are needed (for everyone, or for one department), the employee uploads a
  file, HR approves or rejects it.
- An employee directory for HR, with a detail page per employee to manage
  that person's required documents.
- A profile page with your info, your department's Slack link, and your
  document checklist.

**How did you prioritize them?**

I prioritized around the actual problem in the brief: a new hire who knows
no one, doesn't know how things work, and starts in three days. So the first
things I built were the ones that answer "what do I need to do and know
before/on day one" - accounts and the document checklist - followed by
announcements (what's going on) and the calendar (when am I expected and
what's already on my schedule). The employee directory and document-type
management came later, since HR needs to set that data up before an
employee can use any of it. This also roughly matches the order I actually
built the API endpoints in.

**Which features did you intentionally leave out of scope?**

- Letting a colleague open someone else's calendar to book or request a
  meeting with them. I considered this early on, but it turned into a much
  bigger feature (checking availability, sending a request, accepting it)
  than the time allowed, so right now only whoever creates an event picks
  the participants.
- Automatically declining someone's event invite if they mark themselves
  "absent" that day. A nice idea, but work mode and event status are stored
  separately, and I did not have time to safely connect the two.
- A notification center. Right now, if HR approves your document or someone
  invites you to an event, you only see it by opening the app - nothing
  pushes it to you.
- Input validation beyond "this field is required." Nothing stops HR from
  typing a malformed email, for example.
- Profile pictures, and downloadable templates for document types.
- Limiting the announcement feed to posts made after your hire date.

## Technical decisions

**Why did you choose this database structure?**

The tables mirror the actual things in the problem: Accounts (each with a
Role and a Department), Roles, Departments, Posts/Comments/Reacts,
Documents/DocumentTypes, Events/EventParticipants, and WorkSchedule. These
are simple, mostly one-to-many relationships, so a relational database with
foreign keys was a natural fit, and I didn't see a reason to reach for
anything more exotic. I used SQL Server with EF Core migrations, which made
it easy to change the schema as the design changed instead of writing SQL by
hand. Two rules I enforced at the database level on purpose, not just in
code: you can't react twice to the same post, and you can't set two
different work modes for the same day.

**Why did you choose these libraries/frameworks?**

Backend: ASP.NET Core Web API on .NET 8, split into Controllers, Services,
and Repositories, with small DTOs and dedicated mapper classes (Mapperly)
between them so controllers never touch database entities directly. JWT for
login, and ASP.NET's own PasswordHasher so passwords are salted and hashed,
never stored as plain text. Frontend: Angular, using its newer
standalone-component and signals style rather than the older NgModule
pattern, and hand-written SCSS instead of a component library like Material
or Bootstrap, so it would look like its own product rather than a generic
template.

**If you had more time, what would you build differently?**

A few things I'd fix or add: a notifications system, so status changes show
up without the user having to refresh and check; move the frontend's API
address out of hardcoded strings and into a proper environment file; and
actually use FluentValidation, which I added as a dependency early on
planning to validate incoming requests with it, but never got around to
wiring up - right now it does nothing. I'd also revisit permissions: my
original plan had them tied to department as well as role, and I simplified
that down to just role while building, which was the right call for the
time I had, but is worth reconsidering.

## UX decisions

**Why did you choose this user flow?**

The navigation itself reflects the two types of users: a regular employee
sees Announcements, Calendar, and My Profile; HR sees those plus New
Employee, Employees, and Document Types. The very first thing that can
happen after logging in with a temporary password is a forced "change your
password" screen, before anything else in the app is reachable - so the new
hire's first action is taking ownership of their own account. After that,
the announcements feed is the landing page, since the idea is for it to
replace the "Welcome! See you Monday" email with an actual home base.

**Did you test it with anyone?**

No. This was a solo project on a short deadline, so I did not get the
chance to sit a real person down as either "new employee" or "HR" and watch
them use it. I did ask a friend to test if they can run the app locally on their machine.

**What changed after receiving feedback?**

Since there wasn't a formal testing round, my main feedback loop was
comparing my own early notes against what I was actually able to build, and
cutting from there. Colleague-to-colleague calendar booking, a notification
center, and department-based permissions were all in my original plan and
all got dropped once I saw they would take time away from finishing the
core flow - login, documents, announcements, calendar - end to end.
