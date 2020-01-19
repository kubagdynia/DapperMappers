# DapperMappers
An example of using Dapper with the custom Xml and Json mappers. Can be used to serialize and deserialize objects by Dapper.

### Project structure
![](doc/ProjectStructure.png)
- DapperMappers.Api - Sample REST API that uses serialization and deserialization to XML (uses the MS SQL Server Express database)
- DapperMappers.Core - DBConnection extension
- DapperMappers.Domain - Sample domain used by the REST API
- DapperMappers.Domain.Tests - Domain tests using SQLite database

### How to use
- Create class that implements IXmlObjectType or IJsonObjectType interface
```csharp
public class Book
{
	public long Id { get; set; }
	public string Title { get; set; }
	public BookDescription Description { get; set; }
}

public class BookDescription : IXmlObjectType
{
	public Learn Learn { get; set; }
	public string About { get; set; }
	public Features Features { get; set; }
}

public class Learn
{
	public List<string> Points { get; set; }
}

public class Features
{
	public List<string> Points { get; set; }
}
```
- Register these new classes in Startup.cs
```csharp
services.RegisterDapperCustomTypeHandlers(new[] { typeof(Book).Assembly });
```
- Create table in a database that contains a column of the XML type
```sql
CREATE TABLE [dbo].[Books](
	[Id] bigint IDENTITY(1,1) NOT NULL,
	[Title] nvarchar(200) NOT NULL,
	[Description] xml NULL
	CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)
```
- Use Dapper to save object data in the database
```csharp
public async Task SaveBook(Book book)
{
	using (var conn = _connectionFactory.Connection())
	{
		await conn.ExecuteAsync(_@"INSERT INTO Books (Title, Description) VALUES (@Title, @Description)", book);
	}
}
```

### How to Run test REST API
- Download and install [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download)
- Download and install [MS SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express)
- Create an empty database called BaseDB and run [CreateDapperMappersDbSQLServer.sql](https://github.com/kubagdynia/DapperMappers/blob/master/Sql/CreateDapperMappersDbSQLServer.sql) script
- Clone or download source code
> git clone https://github.com/kubagdynia/DapperMappers.git
- Set a database connection string in the API project in the [appsettings.json](https://github.com/kubagdynia/DapperMappers/blob/master/DapperMappers/DapperMappers.Api/appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Localhost\\SQLEXPRESS2014;Initial Catalog=BookDB;Integrated Security=True;MultipleActiveResultSets=True;"
  }  
}
```
- If you run from IDE set the Startup Item to the DapperMappers.API project, not IIS Express
- Run API project from the command line
> dotnet run --project .\DapperMappers\DapperMappers.Api\
- Open the API documentation in the browser
> https://localhost:5001/swagger

![](doc/SwaggerBookAPI.png)

### How to Test
Every commit or pull request is built and tested on the Continuous Integration system ([Travis CI](https://travis-ci.com/kubagdynia/DapperMappers/branches)).

To test locally:
- Download and install [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download)
- Clone or download source code
> git clone https://github.com/kubagdynia/DapperMappers.git
- Start tests from the command line
> dotnet test ./DapperMappers/

### Technologies
List of technologies, frameworks and libraries used for implementation:
- [.NET Core 3.1](https://dotnet.microsoft.com/download) (platform)
- [MS SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions-express) (database)
- [Dapper](https://github.com/StackExchange/Dapper) (micro ORM)
- [Dapper.CustomTypeHandlers](https://github.com/kubagdynia/Dapper.CustomTypeHandlers) (custom handlers)
- [Automapper](https://github.com/AutoMapper/AutoMapper) (object mapper)
- [FluentValidation](https://fluentvalidation.net/) (data validation)
- [System.Text.Json](https://www.nuget.org/packages/System.Text.Json) (JSON serialization/deserialization)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) (Swagger automated API documentation)
- [NUnit](https://nunit.org/) (testing framework)
- [SQLite](https://www.sqlite.org/) (database for testing purpose)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions) (fluent API for asserting the result of unit tests)

### License
This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

### Builds and Tests
| Branch       | Status      |
|--------------|-------------|
| [master](https://travis-ci.com/kubagdynia/DapperMappers/branches)       | [![Build Status](https://travis-ci.com/kubagdynia/DapperMappers.svg?branch=master)](https://travis-ci.com/kubagdynia/DapperMappers)|
| [develop](https://travis-ci.com/kubagdynia/DapperMappers/branches)      | [![Build Status](https://travis-ci.com/kubagdynia/DapperMappers.svg?branch=develop)](https://travis-ci.com/kubagdynia/DapperMappers)|
