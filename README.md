# PetRescue

**Webapp for Pet Adoption in the USA**

---

## Overview

PetRescue is a web application designed to streamline the pet adoption process in the United States. By connecting potential adopters with rescue organizations, our platform makes it easier than ever to browse available pets, submit adoption requests, and manage pet listings. Whether you’re looking to adopt or help an organization manage their pet profiles, PetRescue provides a simple, efficient, and user-friendly experience.

---

## Features

- **User Authentication:** Secure registration and login for adopters and rescue staff.
- **Pet Browsing :** Easily search for pets by  location in the USA.
- **Responsive Design:** Optimized for desktop and mobile devices to ensure a seamless user experience.
- **Saving pets to favorites:** storing your favorite pets in database.
- **Test Coverage:** Comprehensive testing to ensure code quality and maintainability.

---

## Technologies Used

- **Backend:** C#, ASP.NET Core, Entity Framework Core , SQlite
- **Frontend:** HTML, CSS, JavaScript, Boostrap, AOS for animations
- **Database:** SQLite (with support for PostgreSQL or other SQL databases)
- **Other Tools:** .NET SDK, Visual Studio, Git, and standard development utilities

---

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0 or later)
- [Visual Studio](https://visualstudio.microsoft.com/) or your preferred IDE
- SQLite (or an alternative SQL database)
- Basic knowledge of C# and ASP.NET Core

### Installation

   **Clone the Repository:**
   ```bash
   git clone https://github.com/PaulK98/PetRescue.git
   cd PetRescue
   dotnet restore
   dotnet build
   Update the appsettings.json (or appsettings.Development.json) file with your database connection string if necessary.
   WARNING: Use your own api keys from the official petfinder website and insert them to appsettings.json , "ApiKey": "",
    "ApiSecret": "".
   dotnet ef database update
   dotnet run
   ```
