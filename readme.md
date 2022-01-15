# Steps to run the project

1. Clone the project using command or unzip file
2. Build the solution to restore nuget packages
3. Create a mysql database `RoyalCarRentalsDB`
4. Change connection string in `appsettings.json`
5. Start the application from visual studio
6. This will display a Swagger UI, this means api is launched successfully.

======update-database command======
Scaffold-DbContext "Server=DESKTOP-IK08IC5\SQLEXPRESS;Database=RoyalCarRentalsDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force



