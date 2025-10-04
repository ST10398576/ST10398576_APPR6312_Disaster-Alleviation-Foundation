<<<<<<< HEAD
﻿# 🌍 Disaster Alleviation Foundation Web Application  
### A .NET 8.0 MVC Web Application  

This project was developed as part of the **Application Development Practice (ADP470S)** and **Application Development Theory (APT470S)** modules.  
It provides a comprehensive system for managing **disaster incidents, resource donations, and volunteers**, with full authentication and Azure cloud integration.

## 🧭 Overview

The **Disaster Alleviation Foundation (DAF)** web application helps streamline the process of responding to disaster situations by managing:
- **Incident reporting**  
- **Resource donations**  
- **Volunteer registration and management**  

All users are authenticated via a secure login system. Once logged in, they can view and interact with the relevant pages according to their role.

---

## ⚙️ Features

| Feature | Description |
|----------|-------------|
| 👤 **User Authentication** | Secure registration, login, and logout using ASP.NET Identity. |
| 🌪️ **Disaster Incident Reporting** | Users can report new incidents, including type, location, and description. |
| 🎁 **Resource Donations** | Users can donate resources or funds and track their donations. |
| 💪 **Volunteer Management** | Volunteers can register, manage availability, and view assigned tasks. |
| ☁️ **Azure Integration** | Application hosted on **Azure App Service** and connected to **Azure SQL Database**. |
| 🔄 **CI/CD Automation** | Automated build and deployment using **Azure DevOps Pipelines**. |

---

## 🛠️ Technologies Used

| Category | Technology |
|-----------|-------------|
| **Framework** | ASP.NET Core MVC (.NET 8.0) |
| **Frontend** | Razor Pages, Bootstrap 5, HTML5, CSS3 |
| **Backend** | C#, Entity Framework Core |
| **Database** | Azure SQL Database |
| **Authentication** | ASP.NET Identity |
| **DevOps** | Azure Repos, Azure Pipelines |
| **Hosting** | Azure App Service |

---

## 🧩 System Architecture

```text
User → Web Application (ASP.NET Core MVC)
         ↓
    Entity Framework Core (ORM)
         ↓
      Azure SQL Database
         ↓
    Azure App Service (Hosting)
```

##Setup Instructions (Local Development)
### Clone the Repository
git clone https://dev.azure.com/ST10398576/Gift%20of%20the%20Givers%20Foundation/_git/Azure_DisasterAlleviationFoundation

### Open in Visual Studio

Open the .sln file in Visual Studio 2022 or later.

### Update appsettings.json

Add your Azure SQL connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:dafdb.database.windows.net,1433;Initial Catalog=DisasterAlleviationFoundation;Persist Security Info=False;User ID=[Username];Password=[Password];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

### Run Migrations
dotnet ef database update

### Run the App
dotnet run

## Azure Configuration
Resource	          Name	                            Purpose
Web App	            DisasterAlleviationFoundation	    Hosts the web application
SQL Server	        dafdb	                            Manages the database
Database	          DisasterAlleviationFoundation	    Stores users, incidents, donations, and volunteer info
Service Connector	  Azure SQL Connection	            Links the app to the database securely




=======
# 🌍 Disaster Alleviation Foundation Web Application  
### A .NET 8.0 MVC Web Application  

This project was developed as part of the **Application Development Practice (ADP470S)** and **Application Development Theory (APT470S)** modules.  
It provides a comprehensive system for managing **disaster incidents, resource donations, and volunteers**, with full authentication and Azure cloud integration.

## 🧭 Overview

The **Disaster Alleviation Foundation (DAF)** web application helps streamline the process of responding to disaster situations by managing:
- **Incident reporting**  
- **Resource donations**  
- **Volunteer registration and management**  

All users are authenticated via a secure login system. Once logged in, they can view and interact with the relevant pages according to their role.

---

## ⚙️ Features

| Feature | Description |
|----------|-------------|
| 👤 **User Authentication** | Secure registration, login, and logout using ASP.NET Identity. |
| 🌪️ **Disaster Incident Reporting** | Users can report new incidents, including type, location, and description. |
| 🎁 **Resource Donations** | Users can donate resources or funds and track their donations. |
| 💪 **Volunteer Management** | Volunteers can register, manage availability, and view assigned tasks. |
| ☁️ **Azure Integration** | Application hosted on **Azure App Service** and connected to **Azure SQL Database**. |
| 🔄 **CI/CD Automation** | Automated build and deployment using **Azure DevOps Pipelines**. |

---

## 🛠️ Technologies Used

| Category | Technology |
|-----------|-------------|
| **Framework** | ASP.NET Core MVC (.NET 8.0) |
| **Frontend** | Razor Pages, Bootstrap 5, HTML5, CSS3 |
| **Backend** | C#, Entity Framework Core |
| **Database** | Azure SQL Database |
| **Authentication** | ASP.NET Identity |
| **DevOps** | Azure Repos, Azure Pipelines |
| **Hosting** | Azure App Service |

---

## 🧩 System Architecture

```text
User → Web Application (ASP.NET Core MVC)
         ↓
    Entity Framework Core (ORM)
         ↓
      Azure SQL Database
         ↓
    Azure App Service (Hosting)
```

##Setup Instructions (Local Development)
### Clone the Repository
git clone https://dev.azure.com/ST10398576/Gift%20of%20the%20Givers%20Foundation/_git/Azure_DisasterAlleviationFoundation

### Open in Visual Studio

Open the .sln file in Visual Studio 2022 or later.

### Update appsettings.json

Add your Azure SQL connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:dafdb.database.windows.net,1433;Initial Catalog=DisasterAlleviationFoundation;Persist Security Info=False;User ID=[Username];Password=[Password];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

### Run Migrations
dotnet ef database update

### Run the App
dotnet run

## Azure Configuration
Resource	          Name	                            Purpose
Web App	            DisasterAlleviationFoundation	    Hosts the web application
SQL Server	        dafdb	                            Manages the database
Database	          DisasterAlleviationFoundation	    Stores users, incidents, donations, and volunteer info
Service Connector	  Azure SQL Connection	            Links the app to the database securely
>>>>>>> 124881a5ab07a39a04a8079bd1646f42e4e3ffb6
