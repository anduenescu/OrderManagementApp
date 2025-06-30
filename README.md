# OrderManagementApp

The OrderManagementApp is a RESTful Web API built using ASP.NET Core 8. It simulates a basic e-commerce backend and supports operations such as:

- User registration and login (JWT authentication)

- Product browsing, searching, and management

- Cart operations (adding to cart)

- Order placement and reporting (top products, monthly sales)

---
Please refer to:
- `HowToUse.md` for setup and usage instructions
- `APIEndpointOverview.md` for a complete list of available API endpoints.
---
## Technologies Used

### Backend
- ASP.NET Core 8
- Microsoft.Data.SqlClient 5.1.6
- Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0

### Database
- Microsoft SQL Server Express 16.0.1135.2

---

## Solution Architecture

The system follows a layered architecture that enforces single-responsibility and testability:

- **Controllers**: Handle HTTP requests and responses.
- **Services**: Contain business logic and decision-making.
- **Repositories**: Handle direct communication with SQL Server using ADO.NET.

### Architecture Diagram
![Solution Architecture](https://github.com/anduenescu/OrderManagementApp/blob/master/ArchApp.png)

### Component Diagram
![Components](https://github.com/anduenescu/OrderManagementApp/blob/master/ArchComponents.png)

---
### Architectural Context

This app follows a layered architecture:

- Controller Layer – API entry points

- Service Layer – Business logic and rule enforcement

- Repository Layer – ADO.NET-based SQL operations

- Model Layer – Domain entities and DTOs

- Authentication Layer – JWT token-based access control

## Object-Oriented Design

The project includes the following core domain models:

- `User` — Encapsulates user identity and roles (admin or user)
- `Product` — Represents items for sale
- `Order` and `OrderItem` — Represent transactions and their line items
- `Cart` and `CartItem` — Represent a user's temporary shopping selection

Each model respects encapsulation and expresses object relationships (e.g., an Order has a list of OrderItems).

_To be added: Class diagram image to visually explain model relationships_

---

## Authentication Workflow

Authentication is handled using JWT (JSON Web Token):

1. A user sends a `POST` request to `/api/auth/login` with credentials.
2. If valid, the backend issues a token containing username and role.
3. The token must be used in the `Authorization: Bearer <token>` header for protected endpoints.
4. Role-based access (`Admin`, `User`) is enforced using the `[Authorize(Roles = "...")]` attribute.

### Example Login Request

 `/api/auth/login`
```json
{
  "username": "admin",
  "password": "adminpassword"
} 
```
**Response**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```
## Use Case Example: Order Cart Flow

### Endpoint
**POST** `/api/order/ordercart?userId=5`

### High-Level Flow

#### Controller Layer
- The request hits `OrdersController.OrderCart(userId)`
- This calls `_orderService.OrderCart(userId)`

#### Service Layer
In `OrderService.OrderCart(userId)`:
- Retrieves cart items using `_orderRepo.GetUserCart(userId)`
- Validates stock using `ProductService.IsInStock(...)`
- Calculates total price and builds an `Order` object
- Calls `_orderRepo.CreateOrder(order)` to persist it
- Calls `_orderRepo.CleanCart(userId)` to clear the user's cart

#### Repository Layer
- `OrderRepository.GetUserCart` pulls data from `CartItems` table and joins with product info
- `CreateOrder` inserts into `Orders` and `OrderItem` tables
- `CleanCart` removes records from the `CartItems` table for the user

#### Database Layer
- Executes the underlying SQL commands (`INSERT`, `SELECT`, `DELETE`) to store and manage the cart/order lifecycle

---

## Reporting and Advanced Features

### Product Search
- **Endpoint**: `/api/products/search?prefix=lap`
- Uses SQL `LIKE` query to implement prefix filtering

### Top Products Report
- **Endpoint**: `/api/admin/top-products`
- Returns top-selling products using `GROUP BY` and `ORDER BY` SQL logic

### Monthly Sales Report
- **Endpoint**: `/api/admin/monthly-sales`
- Groups orders by month and returns:
  - `Month`
  - `OrderCount`
  - `TotalRevenue`

These reports are served using DTOs (`TopProductDto`, `MonthlySalesDto`) to avoid exposing full model data.

---
### IMPLEMENTATION EXAMPLES

#### Basic Usage

Logging in to get a token:

POST /api/auth/login

Content-Type: application/json
```json
{
  "username": "admin",
  "password": "adminpassword"
}
```

Using the token:

GET /api/products
Authorization: Bearer <your_token>

#### Advanced Scenario: Order Cart

POST /api/order/ordercart?userId=5

#### Steps:

- Gets cart items from repository

- Validates stock via service

- Calculates total

- Places order

---

## Design Patterns Used

### Repository Pattern
- Encapsulates SQL logic in dedicated classes (e.g., `ProductRepository`, `OrderRepository`)

### Service Layer Pattern
- Business logic is managed separately from controllers

### DTO Pattern
- Custom DTOs are used for reporting and API response shaping

---

## Performance Considerations

- Optimized queries using SQL joins and aggregates
- DTOs reduce bandwidth and improve performance
- Static connection strings (to be moved to `appsettings.json` for production use)

---

## Project Structure
- OrderManagementApp/
- ├── Controllers/
- ├── Models/
- │ └── DTOs/
- ├── Repositories/
- ├── Services/
- ├── Exceptions/
- ├── Program.cs
- └── README.md
- Database
- └── README.md
- └── OrderManagementPlatform_schema.sql

