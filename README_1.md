# E-commerce Backend Project

## Project Overview

This is a backend solution for an e-commerce platform built with .NET 8. The project includes core functionalities such as user authentication, product management, category management, and order processing.

## Features

- **User Management**:
  - Register new user
  - User authentication with JWT token
  - Role-based access control (Admin, Customer)
- **Product Management**:
- **Order Management**:


//cartDetails & the cupon will be written in the feature section


## Technologies Used

- **.Net 8**: Web API Framework
- **Entity Framework Core**: ORM for database interactions
- **PostgreSQl**: Relational database for storing data
- **JWT**: For user authentication and authorization
- **AutoMapper**: For object mapping
- **Swagger**: API documentation

## Prerequisites

- .Net 8 SDK
- SQL Server
- VSCode

## Getting Started

### 1. Clone the repository:

```bash
git clone https://github.com/your-username/e-commerce-backend.git
```

### 2. Setup database

- Make sure PostgreSQL Server is running
- Create `appsettings.json` file
- Update the connection string in `appsettings.json`

```json
{
  "ConnectionStrings": {
    "Local": "Server=localhost;Database=ECommerceDb;User Id=your_username;Password=your_password;"
  }
}
```

- Run migrations to create database

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- Run the application

```bash
dotnet watch
```

The API will be available at: `http://localhost:5228`

### Swagger

- Navigate to `http://localhost:5228/swagger/index.html` to explore the API endpoints.

## Project structure

```bash
|-- Controllers: API controllers with request and response
|-- Database # DbContext and Database Configurations
|-- DTOs # Data Transfer Objects
|-- Entities # Database Entities (User, Product, Category, Order)
|-- Middleware # Logging request, response and Error Handler
|-- Repositories # Repository Layer for database operations
|-- Services # Business Logic Layer
|-- Utils # Common logics
|-- Migrations # Entity Framework Migrations
|-- Program.cs # Application Entry Point
```

## API Endpoints

### User

- **POST** `/api/users/register` – Register a new user.
- **POST** `/api/users/login` – Login and get JWT token.

### Category

### Subcategory

### Product 

### Cart
##  

### Payment 
### Coupon

### Order 

### Review 


## Deployment

The application is deployed and can be accessed at: [https://your-deploy-link.com](https://your-deploy-link.com)

## Team Members

- **Leader** : Talal Alqarni (@TalalMAlqarni )
-**Member #1** : Abdulaziz Alsuhaibani (@ama47)
-**Member #2** : Jomana Mahjoob (@wbznan4)
-**Member #3** : Raghad Alessa (@RaghadAdel7)
-**Member #4** : Razan Altowairqi  (@razanmtw17)

## License

This project is licensed under the MIT License.
