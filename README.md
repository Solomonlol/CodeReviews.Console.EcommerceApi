# E-commerce API

A backend-focused e-commerce application built with **C# and ASP.NET Core**, accompanied by a **Console UI client**.

The project was developed as a practical exercise to build a complete REST API with Entity Framework Core, authentication and authorization, database migrations, filtering, sorting, pagination, and a separate client application consuming the API.

The application represents a simplified e-commerce system where users can browse products and categories, create sales, manage their accounts, and where administrators and managers can manage products, categories, users, and sales.

---

## Table of Contents

* [Overview](#overview)
* [Features](#features)
* [Technologies](#technologies)
* [Project Structure](#project-structure)
* [Architecture](#architecture)
* [Domain Model](#domain-model)
* [How the Application Works](#how-the-application-works)
* [Authentication and Authorization](#authentication-and-authorization)
* [Filtering, Sorting and Pagination](#filtering-sorting-and-pagination)
* [Product Attributes](#product-attributes)
* [Sales and Price History](#sales-and-price-history)
* [Database and Entity Framework Core](#database-and-entity-framework-core)
* [Getting Started](#getting-started)
* [Configuration](#configuration)
* [Running the API](#running-the-api)
* [Running the Console Client](#running-the-console-client)
* [Using Swagger](#using-swagger)
* [Using Postman](#using-postman)
* [Seed Data](#seed-data)
* [Example Workflow](#example-workflow)
* [Architectural Decisions](#architectural-decisions)
* [Development Experience and Reflection](#development-experience-and-reflection)
* [Challenges](#challenges)
* [What I Learned](#what-i-learned)
* [Possible Future Improvements](#possible-future-improvements)

---

# Overview

This project is a small e-commerce system consisting of two applications:

1. **ASP.NET Core Web API**
2. **Console UI client**

The Web API is responsible for business logic, data persistence, authentication, authorization, and exposing REST endpoints.

The Console UI acts as a client application. It communicates with the API over HTTP and provides an interactive interface for users.

The main purpose of the project was not to build a production-ready online store, but to practice designing and implementing a multi-layered application with realistic relationships and business rules.

The project includes:

* product management;
* category management;
* product-specific attributes;
* user accounts;
* authentication using JWT;
* role-based authorization;
* sales management;
* shopping cart functionality on the client side;
* filtering;
* sorting;
* pagination;
* Entity Framework Core;
* SQL Server;
* EF Core migrations;
* DTOs;
* dependency injection;
* AutoMapper;
* Swagger/OpenAPI;
* Postman collection;
* a separate Console UI client.

---

# Features

## Products

The API supports:

* retrieving a paginated list of products;
* retrieving a single product;
* creating products;
* updating products;
* deleting products;
* filtering products;
* sorting products;
* adding product attribute values;
* updating product attribute values.

Product creation and modification require appropriate permissions.

---

## Categories

The API supports:

* retrieving categories;
* retrieving a specific category;
* creating categories;
* updating categories;
* deleting categories;
* defining attributes belonging to a category;
* updating category attributes;
* deleting category attributes.

Categories define what type of attributes products in that category can have.

For example, a GPU category may contain attributes such as:

* Brand
* Ray Tracing Cores
* Base Clock
* Boost Clock
* Standard Memory Configuration

while a monitor category may contain completely different attributes.

---

## Users

The application supports:

* user registration;
* authentication;
* retrieving the current user's account;
* retrieving users;
* updating users;
* deleting users;
* filtering users;
* sorting users.

Different users can have different roles.

The current implementation uses:

* `Admin`
* `Manager`
* regular users

Roles are used to restrict access to administrative operations.

---

## Sales

Authenticated users can create sales.

A sale contains multiple products through the `SaleItem` entity.

The system supports:

* creating sales;
* retrieving a specific sale;
* retrieving the current user's sales;
* retrieving all sales for authorized users;
* filtering sales;
* sorting sales;
* pagination;
* closing sales.

Each `SaleItem` stores the quantity and the unit price of the product at the time of the sale.

This is important because product prices can change over time, while historical sales should preserve the price that was actually used when the transaction was created.

---

# Technologies

## Backend

* C#
* .NET 10
* ASP.NET Core Minimal APIs
* Entity Framework Core
* SQL Server
* AutoMapper
* JWT Bearer Authentication
* Swagger / OpenAPI
* Swashbuckle

## Frontend

* C#
* .NET 10
* Console Application
* `HttpClient`
* Dependency Injection
* `Microsoft.Extensions.Hosting`
* Spectre.Console

## Development Tools

* Visual Studio
* SQL Server LocalDB
* Postman
* Git / GitHub

---

# Project Structure

The repository contains two applications.

```text
CodeReviews.Console.EcommerceApi
│
├── Solomonlol.EcommerseApi
│   │
│   ├── Endpoints
│   ├── Interfaces
│   ├── Mapping
│   ├── Migrations
│   ├── Models
│   │   ├── Base
│   │   └── Dto
│   ├── MyResults
│   ├── Seeding
│   ├── Services
│   │   └── Extensions
│   ├── ApplicationContext.cs
│   ├── Program.cs
│   ├── appsettings.json
│   └── postman
│
└── EcommerseAPI.Frontend
    │
    ├── Entities
    ├── Handlers
    ├── Interfaces
    ├── Menus
    ├── MyValidation
    ├── Services
    ├── Program.cs
    └── appsettings.json
```

---

# Architecture

The application follows a service-oriented architecture built around ASP.NET Core Minimal APIs.

The main responsibility of each part is separated as follows:

```text
Console UI
    |
    | HTTP
    v
ASP.NET Core API
    |
    v
Endpoints
    |
    v
Services
    |
    v
Entity Framework Core
    |
    v
SQL Server
```

The API is divided into several logical areas:

### Endpoints

Endpoints define the HTTP API.

They are responsible primarily for:

* receiving HTTP requests;
* extracting parameters;
* calling the appropriate service;
* converting service results into HTTP responses;
* applying authorization requirements.

For example:

```text
ProductEndpoints
CategoryEndpoint
SaleEndpoints
UserEndpoint
LoginEndpoint
```

I intentionally kept most business logic out of the endpoints.

---

## Services

Services contain the main application logic.

Examples include:

```text
ProductService
CategoryService
SaleService
UserService
AccountService
AttributeService
TokenService
```

Interfaces are used to abstract these services:

```text
IProductService
ICategoryService
ISaleService
IUserService
IAccountService
ITokenService
```

This makes the application easier to maintain and makes dependencies explicit.

---

## Entity Framework Core

Entity Framework Core is responsible for communication with the SQL Server database.

The application uses:

* `ApplicationContext`;
* entity models;
* relationships;
* EF Core migrations;
* LINQ queries;
* asynchronous database operations.

---

## DTOs

The API does not directly expose the database entities in every situation.

Instead, DTOs are used for requests and responses.

For example:

```text
ProductDto
ProductAttributeDto
SaleDtoRequest
SaleDtoResponse
SaleItemDtoRequest
SaleItemDtoResponse
UserDtoCreation
UserDtoRequest
UserDtoResponse
```

This provides a separation between the database model and the public API contract.

It also allows request and response models to evolve independently from the underlying entities.

---

# Domain Model

The main entities are:

```text
User
Category
Product
ProductAttribute
ProductAttributeValue
Sale
SaleItem
```

The main relationships are:

```text
Category
   |
   | 1
   |
   | *
 Product
```

A category can contain many products.

---

## Product and Sale

Products and sales have a many-to-many relationship.

Instead of creating a direct many-to-many relationship, I used an explicit junction entity:

```text
Sale
  |
  | 1
  |
SaleItem
  |
  | *
  |
Product
```

`SaleItem` contains:

```text
SaleId
ProductId
Quantity
UnitPrice
```

This approach is necessary because the relationship itself contains additional information.

For example, a sale may contain:

```text
RTX 5090
Quantity: 2
UnitPrice: 1999.99
```

The price belongs to the transaction, not only to the current product.

---

# How the Application Works

A typical request flows through several stages.

For example, when a user creates a sale:

```text
Console UI
    |
    | POST /api/v1/sales
    v
Sale Endpoint
    |
    v
Authentication / Authorization
    |
    v
SaleService
    |
    v
User validation
    |
    v
Product validation
    |
    v
SaleItem creation
    |
    v
Database
```

The API verifies the authenticated user and determines the user's identity from the JWT claims.

The service then validates the requested products and calculates the appropriate sale information.

Finally, the sale and its items are stored in the database.

---

# Authentication and Authorization

The application uses **JWT Bearer authentication**.

The login process is:

```text
User
 |
 | email + password
 v
/api/v1/login
 |
 v
UserService
 |
 | password verification
 v
TokenService
 |
 v
JWT token
```

The token contains information about the authenticated user, including their identity and role.

The client stores the token and sends it with subsequent requests.

Protected endpoints use the `[Authorize]` attribute.

Some endpoints require specific roles.

For example:

```text
[Authorize(Roles = "Admin, Manager")]
```

This means that only administrators and managers can perform certain management operations.

Regular authenticated users can perform operations such as creating sales and viewing their own account information.

---

# Filtering, Sorting and Pagination

The API supports filtering, sorting, and pagination for several resources.

For example, product requests can include:

```text
page
pageSize
filter
sort
```

The API also limits the maximum page size to prevent unnecessarily large responses.

The current implementation uses:

```text
page >= 1
pageSize between 1 and 30
```

This allows clients to request only the data they currently need.

Filtering and sorting logic is separated into dedicated classes and extension methods rather than being placed directly into the endpoint methods.

This keeps endpoint definitions relatively small and makes query-building logic reusable.

---

# Product Attributes

One of the more domain-specific parts of the application is the product attribute system.

Different categories require different properties.

For example:

### CPU

```text
Brand
Core count
Thread count
Base Clock
Max. Boost Clock
TDP
```

### GPU

```text
Brand
Ray Tracing Cores
Base Clock
Boost Clock
Standard Memory Config
```

### Monitor

```text
Display Size
Resolution
Aspect Ratio
Screen Surface
Brand
```

Instead of adding dozens of nullable columns to the `Product` table, I created separate entities:

```text
ProductAttribute
ProductAttributeValue
```

A category defines which attributes are available.

A product then stores values for the attributes applicable to its category.

This allows different product categories to have different sets of specifications without constantly changing the database schema.

---

# Sales and Price History

One of the important design decisions was how to handle product prices.

A product has a current:

```text
Product.Price
```

However, this value should not be used to represent the historical price of an already completed transaction.

For this reason, `SaleItem` also stores:

```text
UnitPrice
```

When a sale is created, the current product price is copied into the corresponding sale item.

Conceptually:

```text
Product
Price = $100
        |
        | purchase
        v
SaleItem
UnitPrice = $100
```

If the product price later changes:

```text
Product
Price = $120
```

the historical sale still contains:

```text
SaleItem
UnitPrice = $100
```

This preserves the financial history of the transaction.

This was an important part of the domain model because simply referencing the current product price would make historical sales incorrect whenever product prices changed.

---

# Database and Entity Framework Core

The application uses **SQL Server** through Entity Framework Core.

The database schema is managed using EF Core migrations.

The repository contains migrations representing the evolution of the database model during development.

For example:

```text
InitialMigration
UpdatingEntities
EntityChange
UserChange
AttributeChange
...
```

Migrations allow the database schema to be recreated and updated consistently from the codebase.

The application also contains database seeding logic.

---

# Getting Started

## Prerequisites

Before running the project, make sure the following are installed:

* .NET 10 SDK
* Visual Studio 2022/2026 or another compatible .NET IDE
* SQL Server LocalDB
* Git
* Postman (optional, but recommended for testing the API)

You can verify the .NET installation with:

```bash
dotnet --version
```

---

# Clone the Repository

Clone the repository:

```bash
git clone <repository-url>
```

Then navigate to the project directory:

```bash
cd CodeReviews.Console.EcommerceApi
```

---

# Database Configuration

The API currently uses SQL Server LocalDB.

The connection string is configured in:

```text
Solomonlol.EcommerseApi/appsettings.json
```

The current development connection string is similar to:

```json
"ConnectionStrings": {
    "MSSQLServer": "Server=(localdb)\\MSSQLLocalDB;Database=EcommerseDb;Trusted_Connection=true;"
}
```

If LocalDB is installed, no separate SQL Server configuration should normally be necessary.

If a different SQL Server instance is used, update the connection string accordingly.

---

# JWT Configuration

JWT configuration is also stored in `appsettings.json`.

Example:

```json
"Jwt": {
    "Key": "<your-secret-key>",
    "Issuer": "MyAuthServer",
    "Audience": "MyAuthClient",
    "ExpireMinutes": 30
}
```

For a real production application, the secret key should **not** be committed to source control.

It should instead be stored using a secure configuration mechanism such as:

* environment variables;
* .NET User Secrets;
* Azure Key Vault;
* another secrets-management solution.

The value shown in the repository is intended only for local development.

---

# Running the API

Open a terminal in:

```text
Solomonlol.EcommerseApi
```

Run:

```bash
dotnet restore
```

Then:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

The API will start using the configured ASP.NET Core launch settings.

Swagger is available in the Development environment.

The exact URL depends on the launch configuration, for example:

```text
https://localhost:<port>/swagger
```

or:

```text
http://localhost:<port>/swagger
```

---

# Running the Console Client

The Console UI is located in:

```text
EcommerseAPI.Frontend
```

Before starting the client, make sure the API is already running.

The client uses the following configuration:

```json
"ApiSettings": {
    "BaseUrl": "http://localhost:5254/"
}
```

If the API runs on a different port, update this value in:

```text
EcommerseAPI.Frontend/appsettings.json
```

Then run:

```bash
dotnet restore
dotnet build
dotnet run
```

The Console UI will start and communicate with the API using HTTP requests.

⚠️ **Important:** on every startup, `SeedDb.SeedAll()` calls `Database.EnsureDeletedAsync()` **followed by** `Database.MigrateAsync()` — meaning the database is dropped and recreated from scratch, then re-seeded, every single time you run the API. Any data you created in a previous session (new users, sales, etc.) will be wiped on the next run. This is convenient for a portfolio project with a rich, deterministic demo catalog, but would need to be removed before this could be used as anything beyond a demo.
4. The seed populates: ~13 categories (CPU, GPU, RAM, Motherboards, Cases, Monitors, Headphones, Fans, Mouse, Keyboards, Laptops, SSD, HDD), a full PC-parts product catalog per category with attributes (e.g. CPU core/thread count, clock speeds; GPU memory; RAM speed/latency, etc.), and 5 demo users. Seeded login/password pairs:
   | Login | Role | Password |
   |---|---|---|
   | `First` | Admin | `Password123` |
   | `Second` | Manager | `Password123` |
   | `Third`, `Fourth`, `Fifth` | User | `Password123` |

---

# Using Swagger

Swagger provides an interactive interface for testing the API.

After starting the API, open:

```text
https://localhost:<port>/swagger
```

Swagger can be used to:

* inspect available endpoints;
* inspect request and response models;
* send requests;
* test authentication-protected endpoints.

For protected endpoints, first authenticate and obtain a JWT token.

Then use the Swagger authorization functionality to provide the token.

---

# Using Postman

A Postman collection is included in the repository.

It can be found in:

```text
Solomonlol.EcommerseApi/postman/
```

The collection contains requests for testing the API.

The recommended testing flow is:

```text
1. Register or use a seeded user
2. Login
3. Obtain JWT token
4. Authorize protected requests
5. Create/read/update products
6. Create/read/update categories
7. Create sales
8. Retrieve sales
9. Test filtering
10. Test sorting
11. Test pagination
```

---

# Seed Data

The application contains database seeding logic.

When the API starts, the database initialization process:

1. creates/recreates the database;
2. applies EF Core migrations;
3. creates categories;
4. creates products;
5. creates users;
6. creates product attributes;
7. creates product attribute values.

This makes the project easier to test because a predefined dataset is available after startup.

The seed process is intended for development/testing rather than production use.

---

# Example Workflow

A typical user workflow looks like this:

```text
1. Start SQL Server LocalDB
        |
        v
2. Start ASP.NET Core API
        |
        v
3. Database is initialized and seeded
        |
        v
4. Start Console UI
        |
        v
5. Login
        |
        v
6. Browse categories
        |
        v
7. Browse products
        |
        v
8. Add products to the shopping cart
        |
        v
9. Create a sale
        |
        v
10. View personal sales history
```

An administrator or manager has additional capabilities:

```text
Login
  |
  +-- Manage products
  |
  +-- Manage categories
  |
  +-- Manage category attributes
  |
  +-- Manage users
  |
  +-- View sales
  |
  +-- Manage sales
```

---

# Architectural Decisions

## Minimal APIs

I chose ASP.NET Core Minimal APIs instead of traditional MVC controllers.

The project was intended to practice building a relatively lightweight REST API, and Minimal APIs provide a concise way of defining endpoints.

For example:

```csharp
app.MapGet("api/v1/products", ...);
app.MapPost("api/v1/products", ...);
```

This keeps routing and HTTP-related code straightforward.

At the same time, I did not put all business logic into the endpoint definitions. The actual application logic is delegated to services.

---

## Service Layer

One of the main architectural decisions was separating HTTP handling from business logic.

Instead of having an endpoint perform database queries directly, the endpoint calls a service:

```text
Endpoint
   |
   v
IProductService
   |
   v
ProductService
   |
   v
ApplicationContext
```

This makes the code easier to reason about and keeps endpoints relatively small.

It also means that the service layer can potentially be reused by other clients.

---

## Dependency Injection

The application uses ASP.NET Core's built-in dependency injection system.

Services are registered in `Program.cs`:

```csharp
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISaleService, SaleService>();
```

Dependencies are then provided automatically by ASP.NET Core.

I used interfaces for the main services to reduce coupling between components.

---

## DTOs

DTOs were introduced to avoid exposing the database entities directly through every API operation.

This provides better control over:

* input data;
* output data;
* API contracts;
* data that is allowed to be modified.

It also helps avoid coupling the external API directly to the database schema.

---

## AutoMapper

AutoMapper is used to simplify mapping between entities and DTOs.

This reduces repetitive mapping code, especially for objects with many properties.

However, I learned during development that AutoMapper can also make errors harder to understand when mappings are missing or configured incorrectly.

Because of this, mapping configuration needs to be treated as an important part of the application rather than something completely automatic.

---

## Result Pattern

The services use a custom result abstraction for communicating success and failure:

```text
Result<T>
```

Instead of relying exclusively on exceptions for expected application failures, services can return a result containing either:

```text
Success + value
```

or:

```text
Failure + error
```

The endpoint then translates this result into an appropriate HTTP response such as:

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
404 Not Found
409 Conflict
```

---

## Explicit Many-to-Many Entity

For the relationship between products and sales, I deliberately used an explicit `SaleItem` entity rather than a simple many-to-many relationship.

This was necessary because the relationship has its own data:

```text
Quantity
UnitPrice
```

This also makes the domain model more realistic.

---

## Dynamic Product Attributes

I decided not to add category-specific columns directly to the `Product` entity.

For example, having properties like:

```text
CpuCoreCount
GpuMemory
MonitorResolution
KeyboardLayout
...
```

would result in a large number of properties that only apply to particular categories.

Instead, the application uses:

```text
Category
    |
    +-- ProductAttribute
             |
             +-- ProductAttributeValue
```

This allows different categories to define different specifications.

---

# Development Experience and Reflection

This project was significantly more complex than my previous projects because it required me to work with several parts of the .NET ecosystem at the same time.

At the beginning, my main goal was to build a working CRUD Web API with Entity Framework Core.

However, as the project developed, I wanted to make it closer to a real application rather than just implementing the minimum requirements.

I added:

* authentication;
* authorization;
* roles;
* filtering;
* sorting;
* pagination;
* dynamic product attributes;
* a Console UI;
* a shopping cart;
* Swagger authentication;
* database seeding;
* a more structured service architecture.

This increased the complexity of the project considerably, but it also made the development process much more educational.

---

# Challenges

One of the biggest challenges was understanding how the different layers of the application should interact.

Initially, it was easy to think about the API as:

```text
Endpoint -> Database
```

but as the application became more complex, this approach became difficult to maintain.

Introducing services created a clearer structure:

```text
Endpoint -> Service -> Database
```

I also had to learn how dependency injection works in a more realistic application.

For example, the Console UI has its own services and dependency injection configuration, while the API has a separate DI container.

This helped me understand that dependency injection is not just something used by ASP.NET Core controllers. It can also be used in console applications.

---

## Authentication Challenges

JWT authentication was another major challenge.

I had to understand several concepts that were initially unfamiliar to me:

* authentication vs authorization;
* JWT claims;
* roles;
* token generation;
* token validation;
* bearer authentication;
* authentication middleware;
* authorization middleware;
* how the client sends the token;
* how the API extracts the current user from claims.

The final flow is approximately:

```text
Login
  |
  v
Validate credentials
  |
  v
Generate JWT
  |
  v
Client stores token
  |
  v
Client sends Authorization header
  |
  v
JWT middleware validates token
  |
  v
ClaimsPrincipal is created
  |
  v
Endpoint checks authorization
```

Working through this process helped me understand what actually happens behind the `[Authorize]` attribute.

---

# Database Design Challenges

The database model also required more thought than in my previous projects.

The most interesting part was the relationship between products and sales.

Initially, it may seem sufficient to store:

```text
ProductId
SaleId
```

but a real transaction also needs information about:

```text
Quantity
UnitPrice
```

This led me to use `SaleItem` as an explicit entity.

The price-history requirement was especially useful because it forced me to think about the difference between the **current state of a product** and the **historical state of a completed transaction**.

---

# Working With EF Core Migrations

The project went through multiple iterations of the data model.

As requirements changed, I had to modify entities and generate new migrations.

This gave me practical experience with:

* creating migrations;
* applying migrations;
* changing relationships;
* adding properties;
* changing database structures;
* understanding how EF Core tracks model changes.

It also showed me that changing the database model late in development can affect many other parts of the application.

---

# Working With the Console Client

Building the Console UI was useful because it forced me to treat the API as an actual external service.

Instead of calling services directly, the frontend communicates with the API through HTTP.

This introduced additional concerns:

* HTTP status codes;
* authentication headers;
* JSON serialization;
* API errors;
* network failures;
* token handling;
* client-side validation;
* API URL configuration.

This made the separation between the frontend and backend much clearer.

---

# What I Learned

The biggest lesson from this project was that building an application is significantly more complicated than implementing individual features.

I learned more about how different parts of a .NET application interact:

```text
HTTP
 ↓
ASP.NET Core
 ↓
Authentication / Authorization
 ↓
Endpoint
 ↓
Service
 ↓
Entity Framework Core
 ↓
SQL Server
```

I also gained more practical experience with:

* REST API design;
* HTTP status codes;
* Minimal APIs;
* dependency injection;
* interfaces;
* Entity Framework Core;
* database relationships;
* migrations;
* DTOs;
* AutoMapper;
* JWT authentication;
* role-based authorization;
* claims;
* filtering;
* sorting;
* pagination;
* asynchronous programming;
* exception handling;
* API clients;
* application configuration.

---

# Lessons From Mistakes

The development process also showed me that architectural decisions made early can have consequences later.

For example, when the project was smaller, putting more logic directly into certain parts of the application did not seem problematic.

As more features were added, however, it became increasingly important to separate responsibilities.

I also encountered problems related to dependency injection, AutoMapper configuration, JWT configuration, Swagger authentication, and client-side service registration.

These errors were frustrating at the time, but they helped me understand how the .NET dependency injection container and middleware pipeline actually work instead of simply following tutorials.

One of the most useful aspects of these problems was having to trace errors from the exception message back through the application architecture.

---

# What I Would Do Differently

If I started the project again, I would spend more time designing the architecture and database model before implementing the endpoints.

In particular, I would define:

* the domain relationships;
* authorization rules;
* DTOs;
* service responsibilities;
* filtering/sorting strategy;
* database constraints

earlier in the development process.

I would also introduce automated tests much earlier.

During this project, a lot of functionality was tested manually using Swagger, Postman, and the Console UI.

While this works for a small project, automated unit and integration tests would make refactoring safer and would help detect regressions.

---

# Possible Future Improvements

There are several things I would improve if I continued developing the project.

## Automated Tests

Add:

* unit tests for services;
* integration tests for API endpoints;
* authentication tests;
* authorization tests;
* validation tests.

---

## Better Configuration and Secrets Management

Move sensitive configuration such as the JWT signing key out of `appsettings.json`.

For local development:

```text
.NET User Secrets
```

could be used.

For production:

```text
Environment variables
Azure Key Vault
```

or another secrets manager would be more appropriate.

---

## Better Error Handling

Introduce centralized exception handling and a consistent API error response format.

For example:

```json
{
    "status": 400,
    "message": "Invalid product data",
    "errors": []
}
```

This would make error handling more predictable for API clients.

---

## Logging

Introduce structured logging instead of relying mainly on console output.

This would make it easier to investigate problems in a production environment.

---


# Conclusion

This project was an important step in my learning process because it moved beyond small standalone applications and required me to combine multiple concepts into one system.

The main goal was not only to make the application work, but also to understand why different architectural approaches are used and how the components interact.

At some point, this project began to cause frustration, but that was the result of my own decisions, and I learned a lesson from it.

The project gave me practical experience with building a complete .NET application consisting of a Web API, database, authentication system, and separate client.

The most valuable part of the experience was learning to deal with problems that were not isolated programming exercises.


---
