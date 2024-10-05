# E-commerce Backend Project

## Project Overview

This is a backend solution for an e-commerce platform built with .NET 8. The project includes core functionalities such as user authentication, product management, category management, and order processing.

## Features

- **User Management**
  - Create new user
  - User authentication with JWT token
  - Role-based access control (Admin, Customer)
  - Display all users
  - Display specific user
  - Update user information
  - Check for user Username , Email and phone number 
  - Check null values
  - Delete specific user
 
- **Product Management**:
- **Order Management**:

- **SubCategory**
  - Search Subcategories with Pagination.
  - Retrieve all Products within a Subcategory.
  - Add/Update/Delete Products within a Subcategory.
  
- **Payment**
  - Adding a payment with active coupon(isActive: ture). 
  - Adding a payment with unactive coupon(isActive: false). (e.g. using outdated/wrong coupon)  

//cartDetails & the coupon will be written in the feature section


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
|-- Controllers # API controllers with request and response
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

- **POST** `/api/v1/Users` – Creat a new user.
- **POST** `/api/v1/Users/signIn` – Login and get JWT token.
- **GET** `/api/v1/Users` - Display all users
- **GET** `/api/v1/Users/{id}` - Display specific user by Id
- **PUT** `/api/v1/Users/{id}` - Update user information by Id
- **DELETE** `api/v1/Users/{id}` - Delete user by Id

### Category

- **POST** `/api/v1/Categories` – Creat a new category.
- **GET** `/api/v1/Categories` - Retrieve all categories.
- **GET** `/api/v1/Categories/{id}` - Retrieve a specific category by its ID, including its details.
- **PUT** `/api/v1/Categories/{id}` - Update an existing category by its ID.
- **DELETE** `/api/v1/Categories/{id}` - Delete a category by its ID.

### Subcategory

- **POST** `/api/v1/SubCategories` – Creat a new subcategory.
- **POST** `/api/v1/SubCategories/{subCategoryId}/products` - Create and add a new product under a subcategory.
- **GET** `/api/v1/SubCategories` - Retrieve all subcategories.
- **GET** `/api/v1/SubCategories/{id}` - Retrieve a specific subcategory by its ID, including its details.
- **GET** `/api/v1/SubCategories/products` - Retrieve all products inside subcategories. 
- **GET** `/api/v1/SubCategories/products/{productId}` - Retrieve a specific product by its ID inside a subcategory.
 - **GET** `/api/v1/SubCategories/search` - Search for subcategories with pagination.
- **PUT** `/api/v1/SubCategories/{subCategoryId` - Update an existing subcategory by its ID.
- **PUT** `/api/v1/SubCategories/products/{productId}` - Update an existing product by its ID inside a subcategory.
- **DELETE** `/api/v1/SubCategories/{subCategoryId}` - Delete a subcategory by its ID.
- **DELETE** `/api/v1/SubCategories/products/{productId}` - Delete a product by its ID inside a subcategory.

#### Product

### Subcategory

### Product 

### Cart
 

### Payment

- **POST** `/api/v1/payments` - Create a new payment.
- **GET** `/api/v1/payments/{paymentId}` - Retrieve a specific payment by ID
- **POST** `/api/v1/payments `Create a new payment.
- **PUT** `/api/v1/payments/{paymentId}` - Update a payment by ID.
- **DELETE** `/api/v1/payments/{paymentId}` - Delete a payment by ID.

### Coupon
**POST** `/api/v1/coupons` - Create a new coupon.
**GET** `/api/v1/coupons` - Retrieve all coupons.
**GET** `/api/v1/coupons/{id}` - Retrive a specific coupon by ID.
**PUT** `/api/v1/coupons/{id}` - Update a coupon by ID.
**DELETE** `/api/v1/coupons/{id}` - Delete a coupon by ID.

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
