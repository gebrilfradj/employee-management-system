Welcome To My Employee/Intern management system. I created this during off/free time during my internship at ups to give my supervisor a way to keep track of all new interns that will come in and out during the next few years. During the final week of my internship, my supervisors were given this project and they have been very happy with it. 

# Employee Management System

## Description
This Employee Management System is designed using ASP.NET Core and Blazor to streamline the management of employee records, including adding, editing, deleting, and viewing operations. It also features audit logging to monitor changes effectively and uses MSSQL for data management.

## Features
- **CRUD Operations**: Create, Read, Update, and Delete employee details.
- **Audit Logging**: Tracks changes to the employee records with detailed logs.
- **Responsive Web Design**: Implemented using Blazor for a seamless user experience across different devices.
- **Authentication and Authorization**: Manages access control ensuring that only authorized personnel can make changes.

## Technology Stack
- **Frontend**: Blazor Server
- **Backend**: ASP.NET Core
- **ORM**: Entity Framework Core
- **Database**: Microsoft SQL Server
- **Testing**: xUnit, Moq

## Getting Started

### Prerequisites
- .NET 7
- SQL Server

### Installation

1. **Clone the repository:**

2. **Navigate to the project directory:**

3. **Set up the database:**
- Make sure SQL Server is running.
- Create a new database via SQL Server Management Studio or any preferred SQL client.
- Update the connection string in `appsettings.json` or your environment-specific configuration to match your database credentials and server.

4. **Apply migrations to set up your database schema:**


### Running the Application

1. **Start the ASP.NET Core backend:**

- Ensure your backend API is configured to run on an appropriate port which is accessible by your Blazor frontend.

2. **Launch the Blazor App:**

- If using Blazor Server, the `dotnet run` command in the root will host both the backend APIs and the Blazor server-side UI.

### Demonstration

Youtube Link: 
