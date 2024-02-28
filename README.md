# ASP.NET ZERO - ASP.NET Core Version
* Start from here: https://www.aspnetzero.com/Documents/Development-Guide-Core
* Want to port your existing MVC 5.x application? See this post: http://volosoft.com/migrating-from-asp-net-mvc-5x-to-asp-net-core/

Getting up and running on the .net backend/API*
======================================
1. Install and setup PostgresSql and pgAdmin
2. In case you have a new .NET install, you may need to run this as well: ==dotnet tool install --global dotnet-ef==
3. Create a local database for the project using pgAdmin, eg.: ==plateaumed-ehr-local==
4. Update the appsettings.json configuration of the Web.Host project with your default connection string, eg: =="Default": "User ID=xxxx;Password=xxxx;Host=localhost;Port=5432;Database=plateaumed-ehr-local;Include Error Detail=true;"==
5. If the postgresql data provider (Npgsql) does not get installed with your .Net solution.
   Install postgresql data provider for .Net, e.g.:  ==dotnet add package Npgsql==
6. Update database schema with EF migration from the EntityFrameWork project  ==dotnet ef database update --verbose==
7. Ensure Web.Host project is your startup project
8. Run/Debug solution
9. ==.NET 7 SDK== is required for this solutions and it’s recommended to use ==Visual Studio IDE 2022.==

