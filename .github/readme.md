# Greet count app

[![.NET](https://github.com/codex-academy/GreetCountApp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/codex-academy/GreetCountApp/actions/workflows/dotnet.yml)

Setting up GitHub actions with PostgreSQL & C# .Net Core.

## GitHub actions setup

Once you committed your code to GitHub setup CI by using GitHub Actions - click on Actions in your GitHub repository.

Search for  : `.NET By GitHub Actions Build and test a .NET or ASP.NET Core project.

Click on: Setup this workflow

### Add database config for database

Create a services section just below `runs-on` in your `dotnet.yml` file. 

```yml
services:
      postgres:
        image: postgres:latest
        env: 
          POSTGRES_USER: counter
          POSTGRES_PASSWORD: counter123
          POSTGRES_DB: counter_app
        ports:
        - 5432:5432
        # Set health checks to wait until postgres has started
        options: >-
            --health-cmd pg_isready
            --health-interval 10s
            --health-timeout 5s
            --health-retries 5
```

Change the `POSTGRES_` variables as needed.

Add a step to run a database script to create the needed tables.

```yml
- name: create postgresql tables
      run: PGPASSWORD=counter123 psql -h localhost -U counter -d counter_app -a -f ./GreetCount/sql/tables.sql
```

Add an environment variable that matches the database configuration you did earlier to the `Test` task.

```yml
- name: Test
      run: dotnet test --no-build --verbosity normal
      env:
        PSQLConnectionString: Host=localhost;Username=counter;Password=counter123;Database=counter_app
```



