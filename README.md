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
cd src\NetWebApiTemplate.Persistence
dotnet ef database update -- "Data Source=[your_data_source_path]"			
```

### Run
```sh
cd src\NetWebApiTemplate.Api
touch appsettings.Development.json
```

Add this configuration to appsettings.Development.json
```json
{
    "ConnectionStrings": {
        "AppDbContext": "Data Source=[your_data_source_path]"
    }
}
```

Run the Project
```sh
dotnet run
```



