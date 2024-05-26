
# Medicare API

Medicare API is an ASP.NET Core webapi designed to return a list of patients. 

## Table of Contents

- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)


## Installation

1. Clone the repository:

    ```sh
    git clone https://github.com/eglisal/TestCode.git
    cd TestCode
    ```

2. The development environment has been set up. A .env file has been created in the root of the project with the following environment variables:

    ```env
	POSTGRES_DB=medicaredb
	POSTGRES_USER=dbuser
	POSTGRES_PASSWORD=Pa$$word
	ASPNETCORE_ENVIRONMENT=Development
	ConnectionStrings__DefaultConnection=Host=db;Database=medicaredb;Username=dbuser;Password=Pa$$word
	ALLOWED_ORIGINS=http://localhost:8080,http://localhost:8081
    ```

3. Configure the Docker container :

    ```sh
    docker-compose up --build
    ```

## Configuration

The application is configured to allow any origin in the development environment and restrict specific origins in production. You can change the allowed origins by modifying the `ALLOWED_ORIGINS` environment variable.

### CORS Configuration

In the `Program.cs` file:

```csharp
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("SpecificPolicy", policyBuilder => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
    }
    else
    {
        options.AddPolicy("SpecificPolicy", policyBuilder => policyBuilder
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
        );
    }
});
```

### Database Initialization

The `init.sql` file contains SQL instructions to populate the necessary tables. This file is mounted as a volume in the Docker container to initialize the database on startup.


Make sure to mount `init.sql` in the database container in your `docker-compose.yml` file:

```yaml
services:
 db:
    image: postgres:16.2
    env_file:
      - .env
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
      - ./init.sql:/docker-entrypoint-initdb.d/init.sql #Creating tables.
```

## Usage

1. Access the API documentation in Swagger:

    ```sh
    http://localhost:8080/swagger/index.html
    ```

