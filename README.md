1. Start the database:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password!" -p 1433:1433 --name sql1 -h sql1 -d mcr.microsoft.com/mssql/server:2019-latest
```

2. Start the project

```bash
dotnet run
```

3. Execute the following query against the endpoint:

```graphql
query {
  users {
    name
  }
}
```

You can see the projected field in the EF log:

```
Executed DbCommand (10ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
SELECT [u].[Name], [u].[AddressId]
FROM [Users] AS [u]
```
