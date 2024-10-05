# E-commerce Backend Project

## Project Overview

This is a comprehensive backend system solution for an e-commerce website that specializes in selling furniture. The system provide a managable infrastructure that deal with most aspects of the online e-commerce operations, such as product catalog managemnt, order processing..etc.

## Product Dsiplay Structure 
The back-end system features a multi-level product catalog that organizes the furniture inventory. At the top are broad product categories, such as Bedroom and Living Room.

Within each primary category, there are more granular subcategories ,for example, the Bedroom category contains subcategories like Beds, Dressers, and Nightstands.

Housed under these subcategories are the individual product listings. So the Beds subcategory in Bedroom would include specific items like King Bed, single bed, and Kids bed, each with their own detailed information
(Categories -> Subcategories -> Individual Products).

This hierarchical structure allows customers to easily navigate the catalog, drill down into specific product types, and find the exact furniture pieces they need. The backend seamlessly manages this taxonomy of categories, subcategories, and individual products.




## Features

- **User Management**:
  - Register new user
  - User authentication with JWT token
  - Role-based access control (Admin, Customer)

- **Product Management**:
   - Products can be searched by its names. 
   - Products can be filtered based on price range , color , and product name.
   - Products can be sorted based on the added date (New Arrivals) , SKU (low stock) , and Price.
   - The search results highlight the products whose detailed descriptions best align with the user's search query.



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
#### Subcategory
#### Product 
- **POST** `/api/v1/products` - Create new product.
- **GET** `/api/v1/products` - Get all products that matches the sort & filter & search query ,If no query provided, it will return all the products.
- **GET** `/api/v1/{productId}` - Get a specific product by id.
- **PUT** `/api/v1/products/{productId}` - Update product info by id.
- **DELETE** `/api/v1/products/{productId}` - Delete product info by id.


 

### Cart
 

### Payment 
#### Coupon

### Order 

### Review 


## Deployment

The application is deployed and can be accessed at: [https://your-deploy-link.com](https://your-deploy-link.com)

## Team Members

- **Leader** : Talal Alqarni (@TalalMAlqarni )
- **Member #1** : Abdulaziz Alsuhaibani (@ama47)
- **Member #2** : Jomana Mahjoob (@wbznan4)
- **Member #3** : Raghad Alessa (@RaghadAdel7)
- **Member #4** : Razan Altowairqi  (@razanmtw17)

## License

This project is licensed under the MIT License.
