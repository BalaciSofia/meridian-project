# Assumptions

## About the users

**Who uses the application?**

Two kinds of people. Most users are regular employees: the new hire on their
first day, but also everyone else at Meridian, since the app is meant to stay
useful after onboarding is over. The second kind is HR. Meridian
has a single HR person, so I did not build any approval chains or multiple
levels of permission - just "HR/Admin" versus "everyone else". The "Admin"
role exists in the data model for a manager/boss who might also post
announcements, but in practice HR and Admin can do the same things in this
version.

**What does the user already know when opening the application for the
first time?**

Almost nothing about Meridian itself - that is the whole point of the app.
They know their email and a temporary password, because HR created their
account before day one. They do not know their colleagues, their team's
Slack channel, what paperwork they owe, or which days they are expected in
the office.

## About the data

**Who enters the information into the application?**

HR enters the setup data: creates each employee's account (name, email,
temporary password, department), and manages what document types exist and
who they apply to. Departments, roles and document types are already
in place before anyone logs in (I added a starting/seed set, see the
README). Employees enter their own day-to-day data: their office/remote/
absent status for each day, the events they create, and the documents they
upload. Anyone can react to or comment on an announcement.

**When is the information added?**

Account creation happens before the employee's first day, since the brief
says HR already knows someone starts in three days. The required documents
for that employee are attached to their account at the same time, so the
checklist is already waiting for them on day one. Work schedule, events,
comments and reactions are all added continuously, whenever the user opens
the app.

**What happens if information is missing or incorrect?**

Nothing blocks the rest of the app. A document that hasn't been uploaded
just shows as "missing" - the employee can still use everything else. If HR
rejects a submitted document, its status goes back so the employee can
re-upload it. If an employee never sets a work mode for a given day, that
day is simply shown empty on the calendar instead of guessing. I did not add
strict validation beyond the required fields on each form - for a project built in a
few days, I chose to spend the time on features rather than defensive
validation. That would be one of the first things to add with more time.

## About the context

**What device does the new employee use on the first day?**

I assumed a laptop, since Meridian is a hybrid office job, not a mobile-first
consumer product. The layout is responsive down to phone width (the
navigation bar collapses and stacks), but the calendar grid, tables and forms
are designed with a laptop screen in mind first.

**Do they have access to the application before their first working day?**

Yes, on purpose. HR creates the account a few days before the start date, and
there is nothing in the login flow that checks whether today is officially
your first day. So a new hire can log in over the weekend, read the welcome
announcement, see what documents they will need to bring or upload, and check
if any events are already on their calendar - before ever walking into the
office. This is meant to directly replace the "Welcome! See you on Monday"
email with something a lot more useful.

## Others

- I assumed the new employee has not signed any paperwork yet, which is why
  the document checklist exists and starts empty ("missing") for every
  required document type.
- I assumed day-to-day work communication (chat, huddles, quick questions)
  stays in Slack and Google Meet, since the brief lists those as Meridian's
  existing tools. The app links out to each department's Slack channel
  instead of trying to replace it, and events can carry a Google Meet link.
- I originally planned for permissions to also depend on department (per my
  early notes), but simplified this during development to a single
  HR/Admin-vs-employee split, since department-specific permissions were not
  needed once document types could already be scoped to a department.
