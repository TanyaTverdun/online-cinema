# Online Cinema Manager (onlineCinema)

A web application for managing a movie theater. Users can browse the movie catalog and check schedules, while administrators manage the content. The app is deployed on Azure and uses GitHub Actions for automated weekly database updates.

**Live Demo:** (https://cinema-manager-facuf8dzdebsfeb7.polandcentral-01.azurewebsites.net)

## Tech Stack

* **Backend:** C#, ASP.NET Core MVC / Web API
* **Frontend:** Razor Views (.cshtml), CSS, JavaScript, Bootstrap
* **Database & ORM:** MS SQL Server, Entity Framework Core
* **Authentication & Authorization:** ASP.NET Core Identity
* **Media Storage:** Cloudinary (for movie posters)
* **Cloud & DevOps:** Azure App Service, GitHub Actions (automated scheduled database maintenance)

## Features

**For Users:**
* Viewing the movie catalog and detailed information (posters, descriptions, genres, etc).
* Viewing the screening schedule.
* Seat and ticket reservation.
* Reservation of snacks and beverages.
* Ability to cancel reserved tickets and snacks.
* Registration, authorization, and personal profile management.

**For Administrators:**
* Full management of movies.
* Creating and editing the screening schedule.
* Managing the snack assortment.

**Technical & Automation:**
* Weekly automated database update/reset via GitHub Actions using a secure API.
* Storing media files (images/posters) in the Cloudinary cloud storage.

## Local Setup

**Prerequisites:**
* .NET 8.0 SDK
* Visual Studio 2022(or newer)/ JetBrains Rider
* SQL Server Express LocalDB (included with Visual Studio)
* A [Cloudinary](https://cloudinary.com/) account 

**Steps to run the project:**

1. **Clone the repository:**
   ```bash
   git clone https://github.com/TanyaTverdun/online-cinema.git
   ```

2. **Configure the Database:**
   In the main web project (`onlineCinema`), create an `appsettings.Development.json` file right next to the existing `appsettings.json` and add your local connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=OnlineCinemaLocalDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }

3. **Configure Cloudinary:**
   Add your keys secure to User Secrets. Right-click the `onlineCinema` project -> **Manage User Secrets**. This will open a hidden `secrets.json` file. Paste the following configuration inside:
   ```json
   {
     "CloudinarySettings:CloudName": "your_cloud_name",
     "CloudinarySettings:ApiKey": "your_api_key",
     "CloudinarySettings:ApiSecret": "your_api_secret"
   }

4. **Apply Migrations:**
   
   **Using Visual Studio (Package Manager Console):**
   * Right-click the `onlineCinema` project in Solution Explorer and select **Set as Startup Project**.
   * Open the **Package Manager Console** (Tools -> NuGet Package Manager).
   * Change the **Default project** dropdown at the top of the console to `onlineCinema.Infrastructure`.
   * Run the command:
     ```powershell
     Update-Database
     ```
   
   **Using .NET CLI (Terminal):**
   Run the following command from the root folder (where the `.sln` file is located):
   ```bash
   dotnet ef database update --project onlineCinema.Infrastructure --startup-project onlineCinema
   ```

6. **Seed the Database:**
   To populate the database with initial data (roles, admin user, sample movies, etc), open `Program.cs` in the `onlineCinema` project. Insert the following code immediately after the `app.UseRequestLocalization(...)` block:
   ```csharp
   using (var scope = app.Services.CreateScope())
   {
       var services = scope.ServiceProvider;
       try
       {
           var context = services.GetRequiredService<onlineCinema.Infrastructure.Data.ApplicationDbContext>();
           var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<onlineCinema.Domain.Entities.ApplicationUser>>();
           var roleManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();

           await onlineCinema.Infrastructure.Data.DbInitializer.Initialize(context, userManager, roleManager);
       }
       catch (Exception ex)
       {
           var logger = services.GetRequiredService<ILogger<Program>>();
           logger.LogError(ex, "An error occurred while seeding the database.");
       }
   }
   ```

7. **Run the Application:**
   Press `F5` in Visual Studio to start the application and trigger the database seeding. 
   
   **Note:** The first startup may take a bit longer than usual while the database is being created and populated with initial data.
   
   **IMPORTANT:** After the first successful run, **comment out or remove** the seeding block from `Program.cs`. If you leave it active, your local database will be overwritten and reset to its initial state every time you start the application!

## Deployment & Automation

* **Hosting:** The application is deployed and hosted on **Azure App Service**.
* **Scheduled Maintenance:** To keep the live demo clean, a `Weekly Database Update` workflow is configured using **GitHub Actions**. It runs automatically on a schedule and hits a specific API endpoint to reset the database to its initial seeded state.
* **Security:** The database reset endpoint is secured with a custom authorization token. The token is stored safely in Azure Environment Variables and GitHub Secrets.

## Project Structure (Clean Architecture)

The solution is built following the **Clean Architecture** principles.

* **`onlineCinema.Domain`**: The core of the system. It has no dependencies on other layers and contains domain entities, enums, and business constants.
* **`onlineCinema.Application`**: The use-case layer. It contains application services, DTOs, mapping profiles, and defines repository interfaces.
* **`onlineCinema.Infrastructure`**: The external detail layer. It implements the interfaces defined in the Application layer. Contains the `ApplicationDbContext`, EF Core migrations, database seeding (`DbInitializer`), and repository implementations.
* **`onlineCinema` (Web Layer)**: The presentation layer (ASP.NET Core MVC). It depends on the Application and Infrastructure layers, containing Controllers, Views, ViewModels, and Identity configurations.

## The Team

This project was developed collaboratively by a team of 6 members:

* **Tanya Tverdun** - Team Lead, Core Architecture, Cloud Hosting (Azure) & Scheduled Automation (GitHub Actions), Cloudinary Integration, Movie Sessions, Seat & Snack Booking System, Admin Booking Management.
* **Stanislav Sizuk** - Main Movies Catalog, Detailed Movie Pages, and Admin Movie Management (CRUD).
* **Oleksiy Voloshin** - Admin Dashboard & Cinema Halls Management.
* **Maksym Kovalenko** - Identity (Authentication & Registration) and User Profiles.
* **Andriy Hevko** - Admin Data Management (CRUD for Actors, Snacks, Languages, Features, Genres).
* **Ivanna Melko** - Admin Screening & Sessions Management.
