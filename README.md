# NetWebApiTemplate
This is a template project for .NET Web Api. Project structure follow Clean Architecture.'

## Dependencies
- EF Core
- SQLite
- Microsoft Identity
- Swashbuckle

## Layers
- Domain
- Application
- Api
- Infrastructure
- Persistence

## Features
- Authentication with JWT Auth
- Claim Based Authorization

## Setup

### Apply Migration
```sh
dotnet ef database update
```

### Run
```sh
cd src\NetWebApiTemplate.Api
dotnet run
```



