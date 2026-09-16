# GameStop

A simple video game catalog REST API built with ASP.NET Core and Entity Framework Core. This was an early learning project focused on getting the fundamentals right: a clean layered structure, a repository abstraction over EF Core, and a working CRUD API — the stepping stone before [SneakerStore](https://github.com/JakiWZ/SneakerStore), a more complete e-commerce API with authentication and authorization.

## Features

- Full CRUD on a video game catalog (`Name`, `Publisher`, `Developer`, `CopiesCount`, `Price`)
- Repository pattern separating controller logic from EF Core persistence
- DTO-based input on create/update — the client can't set or overwrite the database-generated `Id` (a common beginner mistake, sometimes called "overposting")
- Basic input validation (required name, non-negative price/quantity) with proper `400` responses
- SQLite persistence via EF Core
- Global exception handling middleware
- Unit tests (xUnit + EF Core InMemory) covering the repository and the controller's validation logic

## Tech stack

- ASP.NET Core 10
- Entity Framework Core 10 (SQLite provider)
- xUnit + EF Core InMemory for testing

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run

```bash
cd GameStop.Api
dotnet run
```

### Run the tests

```bash
cd GameStop.Tests
dotnet test
```

## API overview

| Endpoint | Method | Description |
|---|---|---|
| `/api/VideoGame` | GET | List all games |
| `/api/VideoGame/{id}` | GET | Get one game by id |
| `/api/VideoGame` | POST | Create a game |
| `/api/VideoGame/{id}` | PUT | Update a game |
| `/api/VideoGame/{id}` | DELETE | Delete a game |

## What I'd add next

This project intentionally stays scoped to a single-resource CRUD to keep it focused. Authentication, roles, and a richer domain (cart, favorites) are covered in the follow-up project, [SneakerStore](https://github.com/JakiWZ/SneakerStore).
