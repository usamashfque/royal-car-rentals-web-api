# Steps to run the project

1. Clone the project using command `git clone https://bitbucket.org/mrameezraja/doodhkhalis-web-api`
2. Build the solution to restore nuget packages
3. Create a mysql database `doodhkhalis` adnd `doodhkhalis_hangfire`
4. Change connection string in `appsettings.json` file of `DoodhKhalis.Web.Host` project
5. Select `DoodhKhalis.Web.Host` as `Set as startup project`
6. Open `Package Manager Console` and select `DoodhKhalis.EntityFrameworkCore` project in dropdown
7. Run `update-database` command, this will create tables neccessary for application to run
8. Start the application from visual studio
9. This will display a Swagger UI, this means api is launched successfully.
10. Now head towards [angular app](https://bitbucket.org/mrameezraja/doodhkhalis-web-app)
11. Before starting development create your branch "git checkout -b feature/branch_name" 


======update-database command======
Scaffold-DbContext "Server=DESKTOP-IK08IC5\SQLEXPRESS;Database=RoyalCarRentalsDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force



