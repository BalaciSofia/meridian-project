# Meridian Project

This is a small web app to help a new employee get through their first month at
Meridian without feeling lost. It has a company announcement feed, a work
calendar (office / remote / absent, plus events you can accept or decline), a
checklist of onboarding documents to upload, and an employee list for HR.

The project has two parts, and you need both running at the same time:

- `backend` - a .NET 8 API. This talks to the database.
- `frontend` - an Angular app. This is what you see in the browser, and it
  calls the backend API.

## What you need installed first

- .NET 8 SDK
- Node.js (version 20 or newer) and npm
- SQL Server running somewhere you can reach on `localhost:1433`. The
  easiest way is with Docker (see step 1).

## Step 1: Start the database

The backend already expects a SQL Server with these details (you can see them
in `backend/appsettings.json`):

- host: `localhost`, port `1433`
- user: `sa`
- password: `MeridianDb123!`
- database name: `MeridianDb` (this gets created for you automatically, see step 2)

If you don't already have a SQL Server running, the easiest way is to start
one with Docker:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MeridianDb123!" -p 1433:1433 --name meridian-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

You do not need to create the database or the tables yourself. The backend
does this for you the first time it starts.

## Step 2: Run the backend

Open a terminal in the project folder and run:

```bash
cd backend
dotnet restore
dotnet run
```

The first time this runs, it will automatically:

- create the `MeridianDb` database and all its tables
- fill it with some starting data, so the app is not empty: the 5 departments,
  the 3 roles, a few document types, one HR account, and one new-employee
  account (see Step 4 for the logins)

If you restart the backend later, it checks if this data already exists, so
it won't be added twice.

Once it's running, the API is at `https://localhost:7166`. You can open
`https://localhost:7166/swagger` in a browser to see all the endpoints.

If your browser or the frontend complains about the HTTPS certificate not
being trusted, run this once and restart your terminal/browser:

```bash
dotnet dev-certs https --trust
```

## Step 3: Run the frontend

Keep the backend running, and open a **second** terminal:

```bash
cd frontend
npm install
npm start
```

This opens the app at `http://localhost:4200`. Go there in your browser.

## Step 4: Log in

Two accounts are created automatically the first time the backend runs:

HR account (can create employees, manage document types, review submitted documents):
- email: `hr@meridian.com`
- password: `Meridian123!`

New employee account (shows what a brand new hire sees on day one: forced
password change, required documents to upload, etc.):
- email: `newhire@meridian.com`
- password: `Welcome123!`

## A note on the starting data

Every time the backend starts, it checks whether the database is empty. If
it is, it adds:

- 3 roles: Employee, HR, Admin
- 5 departments: Engineering, Sales, Marketing, HR, Finance
- 3 document types that every new employee needs to submit
- the 2 test accounts from Step 4
- one welcome announcement, so the feed is not empty the first time you look at it

This code lives in `backend/Data/DbSeeder.cs`, if you want to look at or
change what gets seeded.

## A couple of things worth knowing

- The database password and the JWT signing key in `backend/appsettings.json`
  are only meant for running this project on your own computer. They are not
  real secrets and should not be used anywhere public.
- The frontend calls the backend at a fixed address (`https://localhost:7166`).
  If you change the backend's port, you will need to update it in the
  frontend code too (search for `localhost:7166` inside `frontend/src`).
